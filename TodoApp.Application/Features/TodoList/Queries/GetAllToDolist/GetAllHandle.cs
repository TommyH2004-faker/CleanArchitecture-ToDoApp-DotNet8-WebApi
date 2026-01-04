using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoList.Queries.GetAllToDolist
{
    public class GetAllHandle : IRequestHandler<GetAllList, Result<List<ToDoListDto>>>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetAllHandle(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<Result<List<ToDoListDto>>> Handle(
            GetAllList request,
            CancellationToken cancellationToken)
        {
            var lists = await _toDoListRepository.GetAllListsAsync();

            var dtoList = lists.Select(list => new ToDoListDto
            {
                ToDoListId = list.ToDoListId,
                Title = list.Title,
                Description = list.Description,
                UserId = list.UserId,
                Items = list.Items.Select(item => new ToDoItemDto
                {
                    Title = item.Title,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted,
                    ToDoListId = item.ToDoListId
                }).ToList()
            }).ToList();

            return Result<List<ToDoListDto>>.Success(dtoList);
        }
    }
}