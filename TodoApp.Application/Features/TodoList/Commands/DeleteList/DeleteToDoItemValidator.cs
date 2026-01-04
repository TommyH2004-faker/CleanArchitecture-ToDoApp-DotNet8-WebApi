namespace TodoApp.Application.Features.TodoList.Commands.DeleteList
{
   public class DeleteToDoValidator
   {
       public bool Validate(DeleteToDoItemListCommand command, out List<string> errors)
       {
           errors = new List<string>();

           if (command.ToDoListId <= 0)
           {
               errors.Add("ToDoListId must be a positive integer.");
           }

           return errors.Count == 0;
       }
   }
}