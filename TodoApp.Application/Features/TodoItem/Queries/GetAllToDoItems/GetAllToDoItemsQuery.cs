using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoItem.Queries.GetAllToDoItems
{
    public class GetAllToDoItemsQuery : IRequest<Result<List<ToDoItemDto>>>
    {
    }
}