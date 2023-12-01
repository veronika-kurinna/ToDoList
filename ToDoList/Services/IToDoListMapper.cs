using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListMapper
    {
        ToDoListItemEntity MapToEntity(ToDoListModel toDoList);
        ToDoListModel MapToModel(ToDoListItemEntity toDoList);
        ToDoListModel MapToModel(CreateToDoListRequest toDoList);
        ToDoListDto MapToDto(ToDoListModel toDoList);
    }
}
