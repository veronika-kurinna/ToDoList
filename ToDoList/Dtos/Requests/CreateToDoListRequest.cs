using System.ComponentModel.DataAnnotations;
using ToDoList.Data.Entities;

namespace ToDoList.Dtos.Requests
{
    public class CreateToDoListRequest
    {
        [Required]
        public string Task { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }
}
