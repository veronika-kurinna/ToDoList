using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListItemService
    {
        Task<IEnumerable<ToDoListItem>> GetAll();
        Task CreateItem(string name);
        Task UpdateItem(int id, string? name, ToDoItemStatuses? status);
    }
}
