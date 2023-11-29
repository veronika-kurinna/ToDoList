using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoListModel>> GetAll();
        Task<int> CreateTask(ToDoListModel toDoListModel);
    }
}
