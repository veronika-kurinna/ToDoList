using ToDoList.Data.Entities;
using ToDoList.Dtos;
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

        public ToDoListItem MapToModel(ToDoListItemEntity item)
        {
            return new ToDoListItem(item.Id, item.Name, item.Status);
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
    }
}
