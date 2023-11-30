using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListMapper
    {
        ToDoListEntity Map(ToDoListModel toDoList);
        ToDoListModel Map(ToDoListEntity toDoList);
        ToDoListModel Map(CreateToDoListRequest toDoList);
        ToDoListDto MapDto(ToDoListModel toDoList);
    }
}
