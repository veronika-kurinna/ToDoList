using FluentAssertions;
using Newtonsoft.Json;
using ToDoList;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Dtos.Requests;
using ToDoList.Dtos.Responses;
using Xunit;

namespace ToDoListIntegrationTest
{
    public class ToDoListItemsControllerTests : IClassFixture<DatabaseFixture<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ToDoListItemsControllerTests(DatabaseFixture<Program> fixture)
        {
            _factory = fixture;
        }

        [Fact]
        public async Task Create_Task_CreatesCorrectly()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            var newTask = new CreateToDoListRequest
            {
                Task = "First Task Test",
                Status = ToDoItemStatuses.InProgress
            };
            var requestToPost = new HttpRequestMessage(HttpMethod.Post, "api/ToDoListItems/Create");
            requestToPost.Content = JsonContent.Create(newTask);

            // Act
            var createResponse = await client.SendAsync(requestToPost);
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var taskId = int.Parse(createResponseString);

            // Assert
            var context = new ToDoListContext(_factory.Options);
            context.ToDoLists.Should().ContainSingle(e => e.Id == taskId &&
                                                          e.Name == newTask.Task &&
                                                          e.Status == newTask.Status);
        }

        [Fact]
        public async Task Get_TwoTasks_ReturnsTwoTasks()
        {
            //Arrange
            HttpClient client = _factory.CreateClient();
            var firstTask = new ToDoListItemEntity
            {
                Name = "First Task Test",
                Status = ToDoItemStatuses.Archived
            };

            var secondTask = new ToDoListItemEntity
            {
                Name = "Second Task Test",
                Status = ToDoItemStatuses.InProgress
            };
            var context = new ToDoListContext(_factory.Options);
            await context.ToDoLists.AddAsync(firstTask);
            await context.SaveChangesAsync();

            await context.ToDoLists.AddAsync(secondTask);
            await context.SaveChangesAsync();

            var requestGet = new HttpRequestMessage(HttpMethod.Get, $"api/ToDoListItems/Get");

            //Act
            var responseGet = await client.SendAsync(requestGet);
            var getResponseString = await responseGet.Content.ReadAsStringAsync();
            var getResponseJson = JsonConvert.DeserializeObject<GetToDoListResponse>(getResponseString);

            //Assert
            getResponseJson.ToDoLists.Should().Satisfy(f => f.Id == firstTask.Id &&
                                                            f.Task == firstTask.Name &&
                                                            f.Status == firstTask.Status,
                                                       s => s.Id == secondTask.Id &&
                                                            s.Task == secondTask.Name &&
                                                            s.Status == secondTask.Status);
        }
    }
}
