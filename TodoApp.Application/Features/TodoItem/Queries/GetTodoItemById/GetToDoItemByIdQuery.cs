using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.TodoItem.Queries.GetToDoItemById
{
    public class GetToDoItemByIdQuery : IRequest<Result<ToDoItemDto>>
    {
        public int Id { get; set; }

        public GetToDoItemByIdQuery(int id)
        {
            Id = id;
        }
    }
}