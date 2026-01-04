using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using static TodoApp.Application.DTOs.ToDoListDto;

namespace TodoApp.Application.Features.TodoList.Commands.UpdateList
{
    public record UpdateCommand(UpdateToDoListDto dto) : IRequest<Result<ToDoListDto>>;
}