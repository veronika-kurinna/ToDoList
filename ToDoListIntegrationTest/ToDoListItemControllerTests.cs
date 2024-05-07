using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using ToDoList;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Dtos.Requests;
using ToDoList.Dtos.Responses;
using ToDoList.Models;
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
                new ToDoListItemEntity{ Name = "Second Item Test", Status = ToDoItemStatuses.Done }
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
            HttpClient client = _factory.CreateClient();
            CreateToDoListItemRequest newItem = new CreateToDoListItemRequest
            {
                Name = "New Item Test"
            };
            HttpRequestMessage requestToPost = new HttpRequestMessage(HttpMethod.Post, "api/ToDoListItem/Create");
            requestToPost.Content = JsonContent.Create(newItem);

            // Act
            HttpResponseMessage createResponse = await client.SendAsync(requestToPost);
            string createResponseString = await createResponse.Content.ReadAsStringAsync();
            CreateToDoListItemResponse? createResponseJson = JsonConvert.DeserializeObject<CreateToDoListItemResponse>(createResponseString);

            // Assert
            createResponseJson.ToDoListItem.Name.Should().Be(newItem.Name);
            createResponseJson.ToDoListItem.Status.Should().Be(ToDoItemStatuses.ToDo);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Create_NameIsEmpty_ReturnsBadRequest(string name)
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            CreateToDoListItemRequest newItem = new CreateToDoListItemRequest
            {
                Name = name
            };
            HttpRequestMessage requestToPost = new HttpRequestMessage(HttpMethod.Post, "api/ToDoListItem/Create");
            requestToPost.Content = JsonContent.Create(newItem);

            // Act
            HttpResponseMessage createResponse = await client.SendAsync(requestToPost);

            // Assert
            createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_SymbolsMoreThan200_ReturnsBadRequest()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            CreateToDoListItemRequest newItem = new CreateToDoListItemRequest
            {
                Name = new string('s', 201)
            };
            HttpRequestMessage requestToPost = new HttpRequestMessage(HttpMethod.Post, "api/ToDoListItem/Create");
            requestToPost.Content = JsonContent.Create(newItem);

            // Act
            HttpResponseMessage createResponse = await client.SendAsync(requestToPost);

            // Assert
            createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_NewName_UpdatesCorrectly()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string initialName = "Item Test";
            ToDoItemStatuses initialStatus = ToDoItemStatuses.Archived;
            ToDoListItemEntity item = new ToDoListItemEntity
            {
                Name = initialName,
                Status = initialStatus
            };

            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.ToDoListItems.AddAsync(item);
            await context.SaveChangesAsync();

            UpdateToDoItemRequest itemToUpdate = new UpdateToDoItemRequest()
            {
                Name = "Updated name"
            };
            HttpRequestMessage requestToUpdate = new HttpRequestMessage(HttpMethod.Put, $"api/ToDoListItem/Update/{item.Id}");
            requestToUpdate.Content = JsonContent.Create(itemToUpdate);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Entry(item).ReloadAsync();
            context.ToDoListItems.Should().Contain(i => i.Name == itemToUpdate.Name &&
                                                        i.Status == initialStatus);
        }

        [Fact]
        public async Task Update_NewStatus_UpdatesCorrectly()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string initialName = "Item Test";
            ToDoItemStatuses initialStatus = ToDoItemStatuses.Done;
            ToDoListItemEntity item = new ToDoListItemEntity
            {
                Name = initialName,
                Status = initialStatus
            };

            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.ToDoListItems.AddAsync(item);
            await context.SaveChangesAsync();

            UpdateToDoItemRequest itemToUpdate = new UpdateToDoItemRequest()
            {
                Status = ToDoItemStatuses.Archived
            };
            HttpRequestMessage requestToUpdate = new HttpRequestMessage(HttpMethod.Put, $"api/ToDoListItem/Update/{item.Id}");
            requestToUpdate.Content = JsonContent.Create(itemToUpdate);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Entry(item).ReloadAsync();
            context.ToDoListItems.Should().Contain(i => i.Name == initialName &&
                                                        i.Status == itemToUpdate.Status);
        }

        [Fact]
        public async Task Update_NewNameAndStatus_UpdatesCorrectly()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string initialName = "Item Test";
            ToDoItemStatuses initialStatus = ToDoItemStatuses.Done;
            ToDoListItemEntity item = new ToDoListItemEntity
            {
                Name = initialName,
                Status = initialStatus
            };

            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.ToDoListItems.AddAsync(item);
            await context.SaveChangesAsync();

            UpdateToDoItemRequest itemToUpdate = new UpdateToDoItemRequest()
            {
                Name = "Updated name",
                Status = ToDoItemStatuses.Archived
            };
            HttpRequestMessage requestToUpdate = new HttpRequestMessage(HttpMethod.Put, $"api/ToDoListItem/Update/{item.Id}");
            requestToUpdate.Content = JsonContent.Create(itemToUpdate);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Entry(item).ReloadAsync();
            context.ToDoListItems.Should().Contain(i => i.Name == itemToUpdate.Name &&
                                                        i.Status == itemToUpdate.Status);
        }

        [Fact]
        public async Task Update_NameWithWhiteSpaces_NotUpdates()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            string initialName = "Item Test";
            ToDoItemStatuses initialStatus = ToDoItemStatuses.ToDo;
            ToDoListItemEntity item = new ToDoListItemEntity
            {
                Name = initialName,
                Status = initialStatus
            };

            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.ToDoListItems.AddAsync(item);
            await context.SaveChangesAsync();

            UpdateToDoItemRequest itemToUpdate = new UpdateToDoItemRequest()
            {
                Name = "   "
            };
            HttpRequestMessage requestToUpdate = new HttpRequestMessage(HttpMethod.Put, $"api/ToDoListItem/Update/{item.Id}");
            requestToUpdate.Content = JsonContent.Create(itemToUpdate);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            await context.Entry(item).ReloadAsync();
            context.ToDoListItems.Should().Contain(i => i.Name == initialName &&
                                                        i.Status == initialStatus);
        }

        [Fact]
        public async Task Update_IdNotExist_ReturnsInternalServerError()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            int id = 208;

            UpdateToDoItemRequest itemToUpdate = new UpdateToDoItemRequest()
            {
                Name = "Updated name",
                Status = ToDoItemStatuses.Archived
            };
            HttpRequestMessage requestToUpdate = new HttpRequestMessage(HttpMethod.Put, $"api/ToDoListItem/Update/{id}");
            requestToUpdate.Content = JsonContent.Create(itemToUpdate);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Update_IdLessThanZero_ReturnsInternalServerError()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            int id = -3;

            UpdateToDoItemRequest itemToUpdate = new UpdateToDoItemRequest()
            {
                Name = "Updated name",
                Status = ToDoItemStatuses.Archived
            };
            HttpRequestMessage requestToUpdate = new HttpRequestMessage(HttpMethod.Put, $"api/ToDoListItem/Update/{id}");
            requestToUpdate.Content = JsonContent.Create(itemToUpdate);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Delete_IdMoreThanZero_DeletesCorrectly()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            ToDoListItemEntity item = new ToDoListItemEntity
            {
                Name = "Item Test",
                Status = ToDoItemStatuses.ToDo
            };

            ToDoListItemContext context = new ToDoListItemContext(_factory.Options);
            await context.AddAsync(item);
            await context.SaveChangesAsync();

            HttpRequestMessage requestToDelete = new HttpRequestMessage(HttpMethod.Delete, $"api/ToDoListItem/Delete/{item.Id}");
            requestToDelete.Content = JsonContent.Create(requestToDelete);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToDelete);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            context.ToDoListItems.Should().NotContain(i => i.Id == item.Id &&
                                                           i.Name == item.Name &&
                                                           i.Status == item.Status);
        }

        [Fact]
        public async Task Delete_IdLessThanZero_ReturnsInternalServerError()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            int id = -2;

            HttpRequestMessage requestToDelete = new HttpRequestMessage(HttpMethod.Delete, $"api/ToDoListItem/Delete/{id}");
            requestToDelete.Content = JsonContent.Create(requestToDelete);

            // Act
            HttpResponseMessage response = await client.SendAsync(requestToDelete);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
