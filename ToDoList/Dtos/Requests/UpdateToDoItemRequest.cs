using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoList.Dtos.Requests
{
    public class UpdateToDoItemRequest
    {
        [MaxLength(200)]
        public string? Name { get; set; }

        [EnumDataType(typeof(ToDoItemStatuses))]
        public ToDoItemStatuses? Status { get; set; }
    }
}
