using ToDoList.Data.Entities;

namespace ToDoList.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }
}
