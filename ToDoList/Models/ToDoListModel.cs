namespace ToDoList.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool ToDo { get; set; }
        public bool InProgress { get; set; }
        public bool Archived { get; set; }
    }
}
