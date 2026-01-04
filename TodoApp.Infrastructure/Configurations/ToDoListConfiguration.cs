using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Configurations
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.ToTable("ToDoLists");
            builder.HasKey(t => t.ToDoListId);

            builder.Property(t => t.ToDoListId)
                .HasColumnName("ToDoListId")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.UserId)
                .HasColumnName("UserId");

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Title");

            builder.Property(t => t.Description)
                .HasMaxLength(500)
                .HasColumnName("Description");

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.Property(t => t.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt");

            // Configure relationships
            builder.HasMany(t => t.Items)
                .WithOne(i => i.ToDoList)
                .HasForeignKey(i => i.ToDoListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}