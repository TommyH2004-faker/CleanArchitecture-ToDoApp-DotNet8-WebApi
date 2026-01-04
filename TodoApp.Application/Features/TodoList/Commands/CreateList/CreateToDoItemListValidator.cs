namespace TodoApp.Application.Features.TodoList.Commands.CreateList;

public class CreateToDoItemListValidator
{
    public bool Validate(CreateToDoItemListCommand command, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(command.ToDoList.Title))
        {
            errors.Add("Title is required.");
        }
        if (command.ToDoList.Description != null && command.ToDoList.Description.Length > 500)
        {
            errors.Add("Description cannot exceed 500 characters.");
        }
        if (command.ToDoList.UserId <= 0)
        {
            errors.Add("Valid UserId is required.");
        }
    

        if (command.ToDoList.ToDoListId < 0)
        {
            errors.Add("ToDoListId cannot be negative.");
        }

        return errors.Count == 0;
    }
    
}