using FluentValidation;

namespace TodoApp.Application.Features.TodoItem.Commands.Update
{
    public class UpdateTodoItemValidator : AbstractValidator<UpdateToDoItemCommand>
    {
        public UpdateTodoItemValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Item)
                .NotNull().WithMessage("Item data is required");

            When(x => x.Item != null, () =>
            {
                RuleFor(x => x.Item.Title)
                    .NotEmpty().WithMessage("Title is required")
                    .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

                RuleFor(x => x.Item.Description)
                    .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

                RuleFor(x => x.Item.Priority)
                    .IsInEnum().WithMessage("Invalid priority value");

                RuleFor(x => x.Item.DueDate)
                    .GreaterThanOrEqualTo(DateTime.Today)
                    .When(x => x.Item.DueDate.HasValue)
                    .WithMessage("Due date cannot be in the past");
            });
        }
    }
}