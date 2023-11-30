using ToDoList.Data.Entities;
using ToDoList.Dtos;
using ToDoList.Dtos.Requests;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListMapper : IToDoListMapper
    {
        public ToDoListEntity Map(ToDoListModel toDoList)
        {
            ToDoListEntity toDoListEntity = new ToDoListEntity()
            {
                Id = toDoList.Id,
                Task = toDoList.Task,
                ToDo = toDoList.ToDo,
                InProgress = toDoList.InProgress,
                Archived = toDoList.Archived
            };
            return toDoListEntity;
        }

        public ToDoListModel Map(ToDoListEntity toDoList)
        {
            ToDoListModel toDoListModel = new ToDoListModel()
            {
                Id = toDoList.Id,
                Task = toDoList.Task,
                ToDo = toDoList.ToDo,
                InProgress = toDoList.InProgress,
                Archived = toDoList.Archived
            };
            return toDoListModel;
        }

        public ToDoListDto MapDto(ToDoListModel toDoList)
        {
            ToDoListDto toDoListDto = new ToDoListDto()
            {
                Id = toDoList.Id,
                Task = toDoList.Task,
                ToDo = toDoList.ToDo,
                InProgress = toDoList.InProgress,
                Archived = toDoList.Archived
            };
            return toDoListDto;
        }

        public ToDoListModel Map(CreateToDoListRequest toDoList)
        {
            ToDoListModel toDoListModel = new ToDoListModel()
            {
                Task = toDoList.Task
            };
            return toDoListModel;
        }
    }
}
