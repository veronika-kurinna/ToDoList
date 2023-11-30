using ToDoList.Data.Entities;

namespace ToDoList.Dtos
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }
}
