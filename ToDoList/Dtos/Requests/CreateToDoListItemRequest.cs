using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dtos.Requests
{
    public class CreateToDoListItemRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
