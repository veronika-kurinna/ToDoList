using System.ComponentModel.DataAnnotations;
using ToDoList.Data.Entities;

namespace ToDoList.Dtos.Requests
{
    public class CreateToDoListItemRequest
    {
        [Required]
        public string Name { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }
}
