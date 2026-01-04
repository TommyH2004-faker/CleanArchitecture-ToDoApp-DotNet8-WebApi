namespace TodoApp.Application.DTOs;
public class ToDoListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int ToDoListId { get; set; }
    public List<ToDoItemDto> Items { get; set; } = new();


    public class CreateToDoListDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ToDoListId { get; set; }
        
        
    }
    public class UpdateToDoListDto
    {
        public string Title { get; set; } = string.Empty;
        public int ToDoListId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
    public class DeleteToDoListDto
    {
        public int ToDoListId { get; set; }
    }
}