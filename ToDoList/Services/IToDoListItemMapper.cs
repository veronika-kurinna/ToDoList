using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListItemMapper
    {
        ToDoListItemEntity MapToEntity(ToDoListItem item);
        ToDoListItem MapToModel(ToDoListItemEntity item);
        ToDoListItemDto MapToDto(ToDoListItem item);
    }
}
