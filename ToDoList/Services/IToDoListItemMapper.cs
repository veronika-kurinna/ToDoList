using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListItemMapper
    {
        ToDoListItemEntity MapToEntity(ToDoListItem item);
        ToDoListItem MapToModel(ToDoListItemEntity itemEntity);
        ToDoListItem MapToModel(CreateToDoListItemRequest itemRequest);
        ToDoListItemDto MapToDto(ToDoListItem item);
    }
}
