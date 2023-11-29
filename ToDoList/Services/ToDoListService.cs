using ToDoList.Data.Repositories;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repository;
        
        public ToDoListService(IToDoListRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ToDoListModel>> GetAll()
        {
            return await _repository.Get();
        }

        public async Task<int> CreateTask(ToDoListModel toDoListModel)
        {
            return await _repository.Create(toDoListModel);
        }
    }
}
