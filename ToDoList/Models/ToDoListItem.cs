using ToDoList.Data.Entities;

namespace ToDoList.Models
{
    public class ToDoListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }
}
