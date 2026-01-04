using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Entities
{
    public class ToDoList
    {
        public ToDoList()
        {
            Items = new List<ToDoItem>();
            Title = string.Empty;
        }

        public int ToDoListId { get; set; }
        public int UserId { get; set; }         // Foreign Key to User
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public  User User { get; set; }          // Navigation property to User
        public ICollection<ToDoItem> Items { get; set; }// a list can have multiple todoitems
        public static ToDoList Create(int userId, string title, string description, int toDoListId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            if (title.Length > 100)
                throw new ArgumentException("Title cannot exceed 100 characters", nameof(title));

            if (description != null && description.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters", nameof(description));
            if (toDoListId < 0)
                throw new ArgumentException("ToDoListId cannot be negative", nameof(toDoListId));
            return new ToDoList
            {
                UserId = userId,
                Title = title,
                Description = description,
                ToDoListId = toDoListId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

    }
}


