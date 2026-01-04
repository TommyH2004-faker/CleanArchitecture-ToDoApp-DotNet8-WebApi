using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using static TodoApp.Application.DTOs.ToDoItemDto;

namespace TodoApp.Application.Features.TodoItem.Commands.CreateTodoItem
{
    public record CreateToDoItemCommand(
    CreateToDoItemDto Item
) : IRequest<Result<ToDoItemDto>>;

}