using FluentAssertions;
using Moq;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories;
using ToDoList.Models;
using ToDoList.Services;
using Xunit;

namespace ToDoListTest
{
    public class ToDoListServiceTests
    {
        private Mock<IToDoListRepository> _repository;
        private IToDoListService _sut;

        public ToDoListServiceTests()
        {
            _repository = new Mock<IToDoListRepository>();
            _sut = new ToDoListService(_repository.Object);
        }

        [Fact]
        public async void GetAll_ThreeTasks_ReturnThreeTasks()
        {
            //Arrange
            List<ToDoListModel> model = new List<ToDoListModel>()
            {
                new ToDoListModel() { Id = 1, Task = "Clean flat", Status = ToDoItemStatuses.Archived},
                new ToDoListModel() { Id = 2, Task = "Read book", Status = ToDoItemStatuses.ToDo},
                new ToDoListModel() { Id = 3, Task = "Call mum", Status = ToDoItemStatuses.InProgress},
            };

            _repository.Setup(s => s.Get())
                        .ReturnsAsync(model);

            //Act
            IEnumerable<ToDoListModel> result = await _sut.GetAll();

            //Assert
            result.Should().Satisfy(
                r => r.Id == 1 && r.Task == "Clean flat" && r.Status == ToDoItemStatuses.Archived,
                r => r.Id == 2 && r.Task == "Read book" && r.Status == ToDoItemStatuses.ToDo,
                r => r.Id == 3 && r.Task == "Call mum" && r.Status == ToDoItemStatuses.InProgress);
        }

        [Fact]
        public async void CreateTask_ThreeTasks_ReturnThreeTasks()
        {
            //Arrange
            ToDoListModel model = new ToDoListModel()
            {
                Id = 1,
                Task = "Read book",
                Status = ToDoItemStatuses.InProgress
            };

            _repository.Setup(s => s.Create(model))
                       .ReturnsAsync(model.Id);

            //Act
            int result = await _sut.CreateTask(model);

            //Assert
            _repository.Verify(r => r.Create(model), Times.Once);
        }
    }
}