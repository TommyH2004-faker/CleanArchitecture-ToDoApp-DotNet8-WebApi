using FluentValidation;

namespace TodoApp.Application.Features.TodoList.Commands.UpdateList
{
    public class UpdateTodoListValidatior : FluentValidation.AbstractValidator<UpdateCommand>
    {
        public UpdateTodoListValidatior()
        {
            RuleFor(x => x.dto.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters");
            RuleFor(x => x.dto.ToDoListId)
                .GreaterThan(0).WithMessage("ToDoListId must be greater than zero");
            RuleFor(x => x.dto.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than zero");
            RuleFor(x => x.dto.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}