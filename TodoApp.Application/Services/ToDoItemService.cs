using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _repository;

        public ToDoItemService(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<ToDoItemDto>>> GetAllItemsAsync()
        {
            try
            {
                var items = await _repository.GetAllItemsAsync();
                var dtos = items.Select(i => i.ToDto());
                return Result<IEnumerable<ToDoItemDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ToDoItemDto>>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result<ToDoItemDto>> GetItemByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return Result<ToDoItemDto>.Failure("Invalid ID");

                var item = await _repository.GetItemByIdAsync(id);
                
                if (item == null)
                    return Result<ToDoItemDto>.Failure("Item not found");

                return Result<ToDoItemDto>.Success(item.ToDto());
            }
            catch (Exception ex)
            {
                return Result<ToDoItemDto>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> AddItemAsync(ToDoItemDto dto)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(dto.Title))
                    return Result.Failure("Title is required");

                var item = ToDoItem.Create(
                    dto.ToDoListId,
                    dto.Title,
                    dto.Description,
                    dto.IsCompleted,
                    dto.DueDate,
                    dto.Priority
                );

                await _repository.AddItemAsync(item);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> UpdateItemAsync(int id, ToDoItemDto dto)
        {
            try
            {
                var item = await _repository.GetItemByIdAsync(id);
                
                if (item == null)
                    return Result.Failure("Item not found");

                item.Update(
                    dto.Title,
                    dto.Description,
                    dto.IsCompleted,
                    dto.DueDate,
                    dto.Priority
                );

                await _repository.UpdateItemAsync(item);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> DeleteItemAsync(int id)
        {
            try
            {
                var item = await _repository.GetItemByIdAsync(id);
                
                if (item == null)
                    return Result.Failure("Item not found");

                await _repository.DeleteItemAsync(id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }
    }
}