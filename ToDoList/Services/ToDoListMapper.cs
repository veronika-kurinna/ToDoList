using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListMapper : IToDoListMapper
    {
        public ToDoListItemEntity MapToEntity(ToDoListModel model)
        {
            ToDoListItemEntity entity = new ToDoListItemEntity()
            {
                Id = model.Id,
                Name = model.Task,
                Status = model.Status
            };
            return entity;
        }

        public ToDoListModel MapToModel(ToDoListItemEntity entity)
        {
            ToDoListModel model = new ToDoListModel()
            {
                Id = entity.Id,
                Task = entity.Name,
                Status = entity.Status,
            };
            return model;
        }

        public ToDoListDto MapToDto(ToDoListModel model)
        {
            ToDoListDto dto = new ToDoListDto()
            {
                Id = model.Id,
                Task = model.Task,
                Status = model.Status
            };
            return dto;
        }

        public ToDoListModel MapToModel(CreateToDoListRequest request)
        {
            ToDoListModel model = new ToDoListModel()
            {
                Task = request.Task,
                Status = request.Status
            };
            return model;
        }
    }
}
