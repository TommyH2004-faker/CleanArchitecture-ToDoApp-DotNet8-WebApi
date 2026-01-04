using MediatR;
using TodoApp.Application.Common;

namespace TodoApp.Application.Features.TodoItem.Commands.Delete
{
    public record DeleteToDoItemCommand(int Id) : IRequest<Result<bool>>;
}