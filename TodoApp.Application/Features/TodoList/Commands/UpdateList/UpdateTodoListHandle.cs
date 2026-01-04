using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoList.Commands.UpdateList
{
    public class UpdateTodoListHandle : IRequestHandler<UpdateCommand, Result<ToDoListDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;
        public UpdateTodoListHandle(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public Task<Result<ToDoListDto>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var existingList = _toDoListRepository.GetListByIdAsync(request.dto.ToDoListId);
            if (existingList == null)
            {
                return Task.FromResult(Result<ToDoListDto>.Failure($"ToDoList with Id {request.dto.ToDoListId} not found"));
            }
            existingList.Result.Title = request.dto.Title;
            _toDoListRepository.UpdateListAsync(existingList.Result);
            var toDoListDto = new ToDoListDto
            {
                ToDoListId = existingList.Result.ToDoListId,
                Title = existingList.Result.Title,
                UserId = existingList.Result.UserId,
                Description = existingList.Result.Description
            };
            return Task.FromResult(Result<ToDoListDto>.Success(toDoListDto));

        }
    }
}