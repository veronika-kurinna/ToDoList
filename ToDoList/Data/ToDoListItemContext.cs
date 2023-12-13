using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Data
{
    public class ToDoListItemContext : DbContext
    {
        public DbSet<ToDoListItemEntity> ToDoListItems { get; set; }

        public ToDoListItemContext(DbContextOptions<ToDoListItemContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoListItemEntityConfiguration());
        }
    }
}
