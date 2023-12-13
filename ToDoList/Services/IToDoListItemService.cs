using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListItemService
    {
        Task<IEnumerable<ToDoListItem>> GetAll();
        Task CreateItem(string name);
        Task UpdateNameOfItem(int id, string name);
        Task UpdateStatusOfItem(int id, ToDoItemStatuses status);
    }
}
