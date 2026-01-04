using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoList.Commands.UpdateList
{
   public record GetbyId(int ToDoListId)
    : IRequest<Result<ToDoListDto>>;

}