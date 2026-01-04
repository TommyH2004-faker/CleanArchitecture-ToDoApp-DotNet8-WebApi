using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Mappings
{
    // Extension methods để map giữa Entity và DTO
    public static class ToDoItemMappingExtensions
    {
        public static ToDoItemDto ToDto(this ToDoItem item)
        {
            return new ToDoItemDto
            {
                ToDoItemId = item.Id,
                ToDoListId = item.ToDoListId,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                DueDate = item.DueDate,
                Priority = item.Priority,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

        public static List<ToDoItemDto> ToDto(this IEnumerable<ToDoItem> items)
        {
            return items.Select(i => i.ToDto()).ToList();
        }
    }
}
