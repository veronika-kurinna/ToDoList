using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dtos.Requests
{
    public class UpdateToDoItemNameRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
