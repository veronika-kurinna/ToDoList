using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Data.Entities
{
    public class ToDoListItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ToDoItemStatuses Status { get; set; }
    }

    public class ToDoListItemEntityConfiguration : IEntityTypeConfiguration<ToDoListItemEntity>
    {
        public void Configure(EntityTypeBuilder<ToDoListItemEntity> builder)
        {
            builder
                .ToTable("ToDoListItems");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Name)
                .HasMaxLength(200);

            builder
                .Property(p => p.Status)
                .HasDefaultValue(ToDoItemStatuses.ToDo);
        }
    }
}
