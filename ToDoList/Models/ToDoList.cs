namespace ToDoList.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool ToDoStatus { get; set; }
        public bool InProgressStatus { get; set; }
        public bool ArchivedStatus { get; set; }
    }
}
