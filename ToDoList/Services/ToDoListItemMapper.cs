using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListItemMapper : IToDoListItemMapper
    {
        public ToDoListItemEntity MapToEntity(ToDoListItem item)
        {
            ToDoListItemEntity itemEntity = new ToDoListItemEntity()
            {
                Id = item.Id,
                Name = item.Name,
                Status = item.Status
            };
            return itemEntity;
        }

        public ToDoListItem MapToModel(ToDoListItemEntity itemEntity)
        {
            ToDoListItem item = new ToDoListItem()
            {
                Id = itemEntity.Id,
                Name = itemEntity.Name,
                Status = itemEntity.Status,
            };
            return item;
        }

        public ToDoListItemDto MapToDto(ToDoListItem item)
        {
            ToDoListItemDto itemDto = new ToDoListItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Status = item.Status
            };
            return itemDto;
        }

        public ToDoListItem MapToModel(CreateToDoListItemRequest itemRequest)
        {
            ToDoListItem item = new ToDoListItem()
            {
                Name = itemRequest.Name
            };
            return item;
        }
    }
}
