using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Data
{
    public class ToDoListContext : DbContext
    {
        public DbSet<ToDoListItemEntity> ToDoLists { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoListEntityConfiguration());
        }
    }
}
