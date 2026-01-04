using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoItem.Commands.CreateTodoItem
{
    public class CreateToDoItemCommandHandlers : IRequestHandler<CreateToDoItemCommand, Result<ToDoItemDto>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public CreateToDoItemCommandHandlers(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<Result<ToDoItemDto>> Handle(CreateToDoItemCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Validate command
                var validationErrors = new List<string>();

                if (command.Item == null)
                    return Result<ToDoItemDto>.Failure("Item data is required");

                if (string.IsNullOrWhiteSpace(command.Item.Title))
                    validationErrors.Add("Title is required");

                if (command.Item.ToDoListId <= 0)
                    validationErrors.Add("ToDoListId is required");

                if (validationErrors.Any())
                    return Result<ToDoItemDto>.Failure(validationErrors);

                // Create entity
                var toDoItem = ToDoItem.Create(
                    toDoListId: command.Item.ToDoListId,
                        title: command.Item.Title,
                        description: command.Item.Description,
                        dueDate: command.Item.DueDate,
                        priority: command.Item.Priority
                );

                // Save to database
                await _toDoItemRepository.AddItemAsync(toDoItem);

                return Result<ToDoItemDto>.Success(toDoItem.ToDto());
            }
            catch (ArgumentException ex)
            {
                return Result<ToDoItemDto>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<ToDoItemDto>.Failure($"An error occurred while creating the ToDoItem: {ex.Message}");
            }
        }
    }
}