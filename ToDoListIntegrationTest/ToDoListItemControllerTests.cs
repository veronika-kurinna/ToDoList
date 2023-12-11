using FluentAssertions;
using Newtonsoft.Json;
using ToDoList;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Dtos.Requests;
using ToDoList.Dtos.Responses;
using Xunit;

namespace ToDoListIntegrationTests
{
    public class ToDoListItemControllerTests : IClassFixture<DatabaseFixture<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ToDoListItemControllerTests(DatabaseFixture<Program> fixture)
        {
            _factory = fixture;
        }

        [Fact]
        public async Task Get_TwoItems_ReturnsTwoItems()
        {
            //Arrange
            HttpClient client = _factory.CreateClient();
            ToDoListItemEntity[] items =
            {
                new ToDoListItemEntity{ Name = "First Item Test", Status = ToDoItemStatuses.ToDo },
                new ToDoListItemEntity{ Name = "Second Item Test", Status = ToDoItemStatuses.InProgress }
            };

            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.ToDoListItems.AddRangeAsync(items);
            await context.SaveChangesAsync();

            HttpRequestMessage requestGet = new HttpRequestMessage(HttpMethod.Get, $"api/ToDoListItem/Get");

            //Act
            HttpResponseMessage responseGet = await client.SendAsync(requestGet);
            string getResponseString = await responseGet.Content.ReadAsStringAsync();
            GetToDoListItemResponse? getResponseJson = JsonConvert.DeserializeObject<GetToDoListItemResponse>(getResponseString);

            //Assert
            getResponseJson.ToDoListItems.Should().Contain(item => item.Id == items[0].Id &&
                                                                   item.Name == items[0].Name &&
                                                                   item.Status == items[0].Status);
            getResponseJson.ToDoListItems.Should().Contain(item => item.Id == items[1].Id &&
                                                                   item.Name == items[1].Name &&
                                                                   item.Status == items[1].Status);
        }

        [Fact]
        public async Task Create_Item_CreatesCorrectly()
        {
            // Arrange
            string f = new String('f', 210);
            HttpClient client = _factory.CreateClient();
            CreateToDoListItemRequest newItem = new CreateToDoListItemRequest
            {
                Name = "New Item Test"
            };
            HttpRequestMessage requestToPost = new HttpRequestMessage(HttpMethod.Post, "api/ToDoListItem/Create");
            requestToPost.Content = JsonContent.Create(newItem);

            // Act
            HttpResponseMessage createResponse = await client.SendAsync(requestToPost);

            // Assert
            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            context.ToDoListItems.Should().Contain(item => item.Name == newItem.Name &&
                                                           item.Status == ToDoItemStatuses.ToDo);
        }
    }
}
