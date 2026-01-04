using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;

namespace TodoApp.Application.Features.TodoItem.Queries.GetToDoItemById
{
    public class GetToDoItemByIdQueryHandler : IRequestHandler<GetToDoItemByIdQuery, Result<ToDoItemDto>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public GetToDoItemByIdQueryHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<Result<ToDoItemDto>> Handle(GetToDoItemByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var toDoItem = await _toDoItemRepository.GetItemByIdAsync(query.Id);
                
                if (toDoItem == null)
                    return Result<ToDoItemDto>.Failure($"ToDoItem with Id {query.Id} not found");

                return Result<ToDoItemDto>.Success(toDoItem.ToDto());
            }
            catch (Exception ex)
            {
                return Result<ToDoItemDto>.Failure($"Error retrieving todo item: {ex.Message}");
            }
        }
    }
}