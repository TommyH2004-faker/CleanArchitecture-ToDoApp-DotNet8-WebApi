using MediatR;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Application.Common;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoList.Commands.DeleteList
{
    public class DeleteToDoItemListCommandHandler : IRequestHandler<DeleteToDoItemListCommand, Result<bool>>
    {
        private readonly IToDoListRepository _toDoListRepository;
        public DeleteToDoItemListCommandHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
         public async Task<Result<bool>> Handle( DeleteToDoItemListCommand request, CancellationToken cancellationToken)
    {
        var existingList =
            await _toDoListRepository.GetListByIdAsync(request.ToDoListId);

        if (existingList == null)
        {
            return Result<bool>.Failure(
                $"ToDoList with Id {request.ToDoListId} not found");
        }

        await _toDoListRepository.DeleteListAsync(request.ToDoListId);

        return Result<bool>.Success(true);
    }
    }
}