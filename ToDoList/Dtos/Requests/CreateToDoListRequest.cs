using ToDoList.Dtos.Attributes;

namespace ToDoList.Dtos.Requests
{
    public class CreateToDoListRequest
    {
        [ValidateNotNullOrNotWhiteSpace]
        public string Task { get; set; }
    }
}
