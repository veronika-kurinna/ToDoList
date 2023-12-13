using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoList.Dtos.Requests
{
    public class UpdateToDoItemStatusRequest
    {
        [Required]
        [Range(0, 3)]
        public ToDoItemStatuses Status { get; set; }
    }
}
