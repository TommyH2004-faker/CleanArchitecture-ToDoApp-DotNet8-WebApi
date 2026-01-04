using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoList.Queries.GetAllToDolist
{
    public record GetAllList : IRequest<Result<List<ToDoListDto>>>
    {
    }
}