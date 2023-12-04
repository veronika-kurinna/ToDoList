using ToDoList.Models;

namespace ToDoList.Data.Repositories
{
    public interface IToDoListItemRepository
    {
        Task<IEnumerable<ToDoListItem>> Get();
        Task Create(ToDoListItem item);
    }
}
