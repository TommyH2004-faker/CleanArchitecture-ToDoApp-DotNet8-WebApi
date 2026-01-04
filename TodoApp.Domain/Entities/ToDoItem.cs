using TodoApp.Domain.Enums;

namespace TodoApp.Domain.Entities
{
    public class ToDoItem
    {
        public int Id { get; private set; }
        public int ToDoListId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? DueDate { get; private set; }
        public PriorityLevel Priority { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; private set; }

        // Navigation property
        public ToDoList ToDoList { get; private set; }

        // Private constructor for EF Core
        private ToDoItem() { }

        // Factory method
        public static ToDoItem Create(int toDoListId, string title, string? description = null, bool isCompleted = false, DateTime? dueDate = null, PriorityLevel priority = PriorityLevel.Medium)
        {
            if (toDoListId <= 0)
                throw new ArgumentException("ToDoListId must be greater than 0", nameof(toDoListId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            if (title.Length > 200)
                throw new ArgumentException("Title cannot exceed 200 characters", nameof(title));

            return new ToDoItem
            {
                ToDoListId = toDoListId,
                Title = title,
                Description = description,
                IsCompleted = false,
                DueDate = dueDate,
                Priority = priority,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public void Update(string title, string? description, bool isCompleted, DateTime? dueDate, PriorityLevel priority)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            if (title.Length > 200)
                throw new ArgumentException("Title cannot exceed 200 characters", nameof(title));

            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsIncomplete()
        {
            IsCompleted = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string title, string description, DateTime? dueDate, PriorityLevel priority)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}