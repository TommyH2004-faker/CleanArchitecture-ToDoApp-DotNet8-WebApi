using MediatR;
using TodoApp.Application.Common;

namespace TodoApp.Application.Features.TodoList.Commands.DeleteList
{
    public record DeleteToDoItemListCommand(
        int ToDoListId
    ) : IRequest<Result<bool>>;
}