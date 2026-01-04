using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Configurations
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
         
            builder.Property(i => i.ToDoListId)
                .HasColumnName("ToDoListId");

            builder.Property(i => i.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Title");

            builder.Property(i => i.Description)
                .HasMaxLength(500)
                .HasColumnName("Description");

            builder.Property(i => i.IsCompleted)
                .HasColumnName("IsCompleted");

            builder.Property(i => i.DueDate)
                .IsRequired(false)
                .HasColumnName("DueDate");

            builder.Property(i => i.Priority)
                .HasColumnName("Priority");

            builder.Property(i => i.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.Property(i => i.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt");

            // Configure relationships
            builder.HasOne(i => i.ToDoList)
                .WithMany(t => t.Items)
                .HasForeignKey(i => i.ToDoListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}