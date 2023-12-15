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

        public async Task CreateItem(string name)
        {
            ToDoListItem toDoListItem = new ToDoListItem(name);
            await _repository.Create(toDoListItem);
        }

        public async Task UpdateItem(int id, ToDoListItem item)
        {
            ToDoListItem itemToUpdate = await _repository.GetById(id);
            itemToUpdate.UpdateItem(item.Name, item.Status);
            await _repository.Update(itemToUpdate);
        }
    }
}
