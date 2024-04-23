using ToDoList.Models;

namespace ToDoList.Data.Repositories
{
    public interface IToDoListItemRepository
    {
        Task<IEnumerable<ToDoListItem>> Get();
        Task<ToDoListItem> GetById(int id);
        Task<int> Create(ToDoListItem item);
        Task Update(ToDoListItem item);
        Task Delete(int id);
    }
}
