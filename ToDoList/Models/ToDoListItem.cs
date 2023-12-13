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

        public ToDoListItem UpdateName(ToDoListItem item, string name)
        {
            Name = name;
            return item;
        }

        public ToDoListItem UpdateStatus(ToDoListItem item, ToDoItemStatuses status)
        {
            Status = status;
            return item;
        }
    }
}
