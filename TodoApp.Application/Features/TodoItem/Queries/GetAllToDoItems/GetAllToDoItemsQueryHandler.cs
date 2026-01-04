using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;

namespace TodoApp.Application.Features.TodoItem.Queries.GetAllToDoItems
{
    public class GetAllToDoItemsQueryHandler : IRequestHandler<GetAllToDoItemsQuery, Result<List<ToDoItemDto>>>
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public GetAllToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<Result<List<ToDoItemDto>>> Handle(GetAllToDoItemsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var toDoItems = await _toDoItemRepository.GetAllItemsAsync();
                var toDoItemDtos = toDoItems.ToDto();
                return Result<List<ToDoItemDto>>.Success(toDoItemDtos);
            }
            catch (Exception ex)
            {
                return Result<List<ToDoItemDto>>.Failure($"Error retrieving todo items: {ex.Message}");
            }
        }
    }
}