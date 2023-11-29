using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Data
{
    public class ToDoListContext : DbContext
    {
        public DbSet<ToDoListEntity> ToDoLists { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoListEntityConfiguration());
        }
    }
}
