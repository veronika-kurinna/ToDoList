using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dtos.Requests
{
    public class CreateToDoListRequest
    {
        [Required]
        public string Task { get; set; }
    }
}
