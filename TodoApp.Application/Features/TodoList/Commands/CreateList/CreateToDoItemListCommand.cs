using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using static TodoApp.Application.DTOs.ToDoListDto;

namespace TodoApp.Application.Features.TodoList.Commands.CreateList
{
    public record CreateToDoItemListCommand(
        CreateToDoListDto ToDoList
    ) : IRequest<Result<ToDoListDto>>;
}