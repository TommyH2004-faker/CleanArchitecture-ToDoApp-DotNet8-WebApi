using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoList.Queries.GetAllByid
{
    public class GetbyIdHandler : IRequestHandler<Commands.UpdateList.GetbyId, Result<ToDoListDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetbyIdHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<Result<ToDoListDto>> Handle(
            Commands.UpdateList.GetbyId request,
            CancellationToken cancellationToken)
        {
            var list = await _toDoListRepository.GetByIdAsync(request.ToDoListId);
            if (list == null)
            {
                return Result<ToDoListDto>.Failure("ToDo List not found.");
            }

           var dto = new ToDoListDto
        {
            ToDoListId = list.ToDoListId,
            Title = list.Title,
            Description = list.Description,
            UserId = list.UserId,
            Items = list.Items
                .Where(item => item.ToDoListId == list.ToDoListId)
                .Select(item => new ToDoItemDto
                {
                    Title = item.Title,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted,
                    ToDoListId = item.ToDoListId
                }).ToList()
        };

            return Result<ToDoListDto>.Success(dto);
        }
    }
}