namespace ToDoList.Dtos
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool ToDo { get; set; }
        public bool InProgress { get; set; }
        public bool Archived { get; set; }
    }
}
