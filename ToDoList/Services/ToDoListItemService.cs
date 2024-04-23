using ToDoList.Data.Repositories;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListItemService : IToDoListItemService
    {
        private readonly IToDoListItemRepository _repository;

        public ToDoListItemService(IToDoListItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ToDoListItem>> GetAll()
        {
            return await _repository.Get();
        }

        public async Task<ToDoListItem> CreateItem(string name)
        {
            ToDoListItem toDoListItem = new ToDoListItem(name);
            return await _repository.Create(toDoListItem);
        }

        public async Task UpdateItem(int id, string? name, ToDoItemStatuses? status)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Id {id} is invalid. Id must be more than zero");
            }
            ToDoListItem itemToUpdate = await _repository.GetById(id);

            if (!string.IsNullOrWhiteSpace(name))
            {
                itemToUpdate.UpdateName(name!);
            }
            if (status.HasValue)
            {
                itemToUpdate.UpdateStatus(status.Value);
            }
            await _repository.Update(itemToUpdate);
        }

        public async Task DeleteItem(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Id {id} is invalid. Id must be more than zero");
            } 
            await _repository.Delete(id);
        }
    }
}
