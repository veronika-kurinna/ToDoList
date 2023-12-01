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
        private Mock<IToDoListItemRepository> _repository;
        private IToDoListItemService _sut;

        public ToDoListServiceTests()
        {
            _repository = new Mock<IToDoListItemRepository>();
            _sut = new ToDoListItemService(_repository.Object);
        }

        [Fact]
        public async void GetAll_ThreeTasks_ReturnThreeTasks()
        {
            //Arrange
            List<ToDoListItem> items = new List<ToDoListItem>()
            {
                new ToDoListItem() { Id = 1, Name = "Clean flat", Status = ToDoItemStatuses.Archived},
                new ToDoListItem() { Id = 2, Name = "Read book", Status = ToDoItemStatuses.ToDo},
                new ToDoListItem() { Id = 3, Name = "Call mum", Status = ToDoItemStatuses.InProgress},
            };

            _repository.Setup(s => s.Get())
                        .ReturnsAsync(items);

            //Act
            IEnumerable<ToDoListItem> result = await _sut.GetAll();

            //Assert
            result.Should().Satisfy(
                r => r.Id == 1 && r.Name == "Clean flat" && r.Status == ToDoItemStatuses.Archived,
                r => r.Id == 2 && r.Name == "Read book" && r.Status == ToDoItemStatuses.ToDo,
                r => r.Id == 3 && r.Name == "Call mum" && r.Status == ToDoItemStatuses.InProgress);
        }

        [Fact]
        public async void CreateTask_ThreeTasks_ReturnThreeTasks()
        {
            //Arrange
            ToDoListItem item = new ToDoListItem()
            {
                Id = 1,
                Name = "Read book",
                Status = ToDoItemStatuses.InProgress
            };

            _repository.Setup(s => s.Create(item))
                       .ReturnsAsync(item.Id);

            //Act
            int result = await _sut.CreateItem(item);

            //Assert
            _repository.Verify(r => r.Create(item), Times.Once);
        }
    }
}