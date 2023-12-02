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
            ToDoListItemEntity firstItem = new ToDoListItemEntity
            {
                Name = "First Item Test",
                Status = ToDoItemStatuses.Archived
            };

            ToDoListItemEntity secondItem = new ToDoListItemEntity
            {
                Name = "Second Item Test",
                Status = ToDoItemStatuses.InProgress
            };
            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.ToDoListItems.AddAsync(firstItem);
            await context.SaveChangesAsync();

            await context.ToDoListItems.AddAsync(secondItem);
            await context.SaveChangesAsync();

            HttpRequestMessage requestGet = new HttpRequestMessage(HttpMethod.Get, $"api/ToDoListItem/Get");

            //Act
            HttpResponseMessage responseGet = await client.SendAsync(requestGet);
            string getResponseString = await responseGet.Content.ReadAsStringAsync();
            GetToDoListItemResponse? getResponseJson = JsonConvert.DeserializeObject<GetToDoListItemResponse>(getResponseString);

            //Assert
            getResponseJson.ToDoListItems.Should().Contain(f => f.Id == firstItem.Id &&
                                                                f.Name == firstItem.Name &&
                                                                f.Status == firstItem.Status);
            getResponseJson.ToDoListItems.Should().Contain(s => s.Id == secondItem.Id &&
                                                                s.Name == secondItem.Name &&
                                                                s.Status == secondItem.Status);
        }


        [Fact]
        public async Task Create_Item_CreatesCorrectly()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            CreateToDoListItemRequest newItem = new CreateToDoListItemRequest
            {
                Name = "New Item Test",
                Status = ToDoItemStatuses.InProgress
            };
            HttpRequestMessage requestToPost = new HttpRequestMessage(HttpMethod.Post, "api/ToDoListItem/Create");
            requestToPost.Content = JsonContent.Create(newItem);

            // Act
            HttpResponseMessage createResponse = await client.SendAsync(requestToPost);
            string createResponseString = await createResponse.Content.ReadAsStringAsync();
            int itemId = int.Parse(createResponseString);

            // Assert
            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            context.ToDoListItems.Should().Contain(e => e.Id == itemId &&
                                                        e.Name == newItem.Name &&
                                                        e.Status == newItem.Status);
        }
    }
}
