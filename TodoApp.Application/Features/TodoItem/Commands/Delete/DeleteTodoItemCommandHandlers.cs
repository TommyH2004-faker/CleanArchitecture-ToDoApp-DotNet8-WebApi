using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoItem.Commands.Delete
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Result<bool>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public DeleteToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<Result<bool>> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if item exists
                var existingItem = await _toDoItemRepository.GetItemByIdAsync(request.Id);
                if (existingItem == null)
                    return Result<bool>.Failure($"ToDoItem with Id {request.Id} not found");

                // Delete item
                await _toDoItemRepository.DeleteItemAsync(request.Id);

                return Result<bool>.Success(true); 
                
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while deleting the ToDoItem: {ex.Message}");
            }
        }
    }
}