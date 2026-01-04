using TodoApp.Domain.Enums;
namespace TodoApp.Application.DTOs;
public class ToDoItemDto
{
    public int ToDoItemId { get; set; }
        public int ToDoListId { get; set; }      // Foreign Key to ToDoList
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public class CreateToDoItemDto
        {
            public int ToDoListId { get; set; }
            public required string Title { get; set; }
            public required string Description { get; set; }
            public DateTime? DueDate { get; set; }
            public PriorityLevel Priority { get; set; }
        }
        public class UpdateToDoItemDto
        {
            public required string Title { get; set; }
            public required string Description { get; set; }
            public DateTime? DueDate { get; set; }
            public bool IsCompleted { get; set; }
            public PriorityLevel Priority { get; set; }
        }
        public class DeleteToDoItemDto
        {
            public int ToDoItemId { get; set; }
        }
}
    