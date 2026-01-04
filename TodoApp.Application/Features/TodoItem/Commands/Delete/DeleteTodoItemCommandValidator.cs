namespace TodoApp.Application.Features.TodoItem.Commands.Delete
{
    using FluentValidation;
   

    public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteToDoItemCommand>
    {
        public DeleteTodoItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id cannot be null.");
  
        }
    }
}
