using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Features.TodoList.Commands.UpdateList;
using TodoApp.Application.Interfaces;

public class UpdateTodoListHandle
    : IRequestHandler<UpdateCommand, Result<ToDoListDto>>
{
    private readonly IToDoListRepository _toDoListRepository;

    public UpdateTodoListHandle(IToDoListRepository toDoListRepository)
    {
        _toDoListRepository = toDoListRepository;
    }

    public async Task<Result<ToDoListDto>> Handle(
        UpdateCommand request,
        CancellationToken cancellationToken)
    {
        var existingList =
            await _toDoListRepository.GetListByIdAsync(request.dto.ToDoListId);

        if (existingList == null)
        {
            return Result<ToDoListDto>.Failure(
                $"ToDoList with Id {request.dto.ToDoListId} not found");
        }

        existingList.Title = request.dto.Title;
        existingList.Description = request.dto.Description;

        await _toDoListRepository.UpdateListAsync(existingList);

        var dto = new ToDoListDto
        {
            ToDoListId = existingList.ToDoListId,
            Title = existingList.Title,
            Description = existingList.Description,
            UserId = existingList.UserId
        };

        return Result<ToDoListDto>.Success(dto);
    }
}
