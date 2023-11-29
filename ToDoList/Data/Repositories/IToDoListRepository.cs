using ToDoList.Models;

namespace ToDoList.Data.Repositories
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoListModel>> Get();
        Task<int> Create(ToDoListModel toDoList);
    }
}
