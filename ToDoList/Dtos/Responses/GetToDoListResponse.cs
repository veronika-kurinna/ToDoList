namespace ToDoList.Dtos.Responses
{
    public class GetToDoListResponse
    {
        public IEnumerable<ToDoListDto> ToDoLists { get; set; }
    }
}
