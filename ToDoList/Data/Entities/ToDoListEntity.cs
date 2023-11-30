using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Data.Entities
{
    public class ToDoListEntity
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }

    public class ToDoListEntityConfiguration : IEntityTypeConfiguration<ToDoListEntity>
    {
        public void Configure(EntityTypeBuilder<ToDoListEntity> builder)
        {
            builder
                .ToTable("ToDoList");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Task)
                .HasMaxLength(200);

            builder
                .Property(p => p.Status)
                .HasDefaultValue(ToDoItemStatuses.ToDo);
        }
    }
}
