using ToDoList.Data.Entities;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListMapper
    {
        ToDoListEntity Map(ToDoListModel toDoList);
        ToDoListModel Map(ToDoListEntity toDoList);
    }
}
