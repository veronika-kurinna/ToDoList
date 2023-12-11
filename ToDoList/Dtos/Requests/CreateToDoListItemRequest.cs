using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dtos.Requests
{
    public class CreateToDoListItemRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
