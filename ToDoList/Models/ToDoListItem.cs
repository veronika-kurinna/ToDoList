namespace ToDoList.Models
{
    public class ToDoListItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ToDoItemStatuses Status { get; private set; }

        public ToDoListItem(string name)
        {
            Name = name;
            Status = ToDoItemStatuses.ToDo;
        }

        public ToDoListItem(int id, string name, ToDoItemStatuses status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
    }
}
