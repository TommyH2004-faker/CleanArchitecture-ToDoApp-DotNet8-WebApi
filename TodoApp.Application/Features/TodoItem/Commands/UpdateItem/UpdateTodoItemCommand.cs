using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using static TodoApp.Application.DTOs.ToDoItemDto;

namespace TodoApp.Application.Features.TodoItem.Commands.Update
{
    public class UpdateToDoItemCommand : IRequest<Result<ToDoItemDto>>
    {
        public int Id { get; set; }
        public UpdateToDoItemDto Item { get; set; }

        public UpdateToDoItemCommand(int id, UpdateToDoItemDto item)
        {
            Id = id;
            Item = item;
        }
    }
}