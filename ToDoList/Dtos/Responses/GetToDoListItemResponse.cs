namespace ToDoList.Dtos.Responses
{
    public class GetToDoListItemResponse
    {
        public IEnumerable<ToDoListItemDto> ToDoListItems { get; set; }
    }
}
