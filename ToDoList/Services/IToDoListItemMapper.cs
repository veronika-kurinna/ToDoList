using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListItemMapper
    {
        ToDoListItemEntity MapToEntity(ToDoListItem item);
        ToDoListItem MapToModel(ToDoListItemEntity itemEntity);
        ToDoListItemDto MapToDto(ToDoListItem item);
    }
}
