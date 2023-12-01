namespace ToDoList.Dtos.Responses
{
    public class GetToDoListResponse
    {
        public IEnumerable<ToDoListItemDto> ToDoListItems { get; set; }
    }
}
