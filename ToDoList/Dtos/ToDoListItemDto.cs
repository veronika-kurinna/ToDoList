using ToDoList.Data.Entities;

namespace ToDoList.Dtos
{
    public class ToDoListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }
}
