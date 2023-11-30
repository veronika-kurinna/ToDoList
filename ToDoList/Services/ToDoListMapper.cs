using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListMapper : IToDoListMapper
    {
        public ToDoListEntity MapToEntity(ToDoListModel model)
        {
            ToDoListEntity entity = new ToDoListEntity()
            {
                Id = model.Id,
                Task = model.Task,
                ToDo = model.ToDo,
                InProgress = model.InProgress,
                Archived = model.Archived
            };
            return entity;
        }

        public ToDoListModel MapToModel(ToDoListEntity entity)
        {
            ToDoListModel model = new ToDoListModel()
            {
                Id = entity.Id,
                Task = entity.Task,
                ToDo = entity.ToDo,
                InProgress = entity.InProgress,
                Archived = entity.Archived
            };
            return model;
        }

        public ToDoListDto MapToDto(ToDoListModel model)
        {
            ToDoListDto dto = new ToDoListDto()
            {
                Id = model.Id,
                Task = model.Task,
                ToDo = model.ToDo,
                InProgress = model.InProgress,
                Archived = model.Archived
            };
            return dto;
        }

        public ToDoListModel MapToModel(CreateToDoListRequest request)
        {
            ToDoListModel model = new ToDoListModel()
            {
                Task = request.Task
            };
            return model;
        }
    }
}
