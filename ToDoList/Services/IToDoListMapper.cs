using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListMapper
    {
        ToDoListEntity MapToEntity(ToDoListModel toDoList);
        ToDoListModel MapToModel(ToDoListEntity toDoList);
        ToDoListModel MapToModel(CreateToDoListRequest toDoList);
        ToDoListDto MapToDto(ToDoListModel toDoList);
    }
}
