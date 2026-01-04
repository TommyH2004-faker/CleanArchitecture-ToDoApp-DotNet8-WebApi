using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Mappings
{
    public static class ToDoListMappingExtensions
    {
        public static ToDoListDto ToDto(this ToDoList toDoList)
        {
            return new ToDoListDto
            {
                ToDoListId = toDoList.ToDoListId,
                Title = toDoList.Title,
                Items = toDoList.Items?.Select(item => item.ToDto()).ToList() ?? new List<ToDoItemDto>()
            };
        }
    }
}