using TodoApp.Application.Common;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repository;

        public ToDoListService(IToDoListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<ToDoList>>> GetAllListsAsync()
        {
            try
            {
                var lists = await _repository.GetAllListsAsync();
                return Result<IEnumerable<ToDoList>>.Success(lists);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ToDoList>>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<ToDoList>>> GetListsByUserIdAsync(int userId)
        {
            try
            {
                var lists = await _repository.GetListsByUserIdAsync(userId);
                return Result<IEnumerable<ToDoList>>.Success(lists);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ToDoList>>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result<ToDoList>> GetListByIdAsync(int id)
        {
            try
            {
                var list = await _repository.GetListByIdAsync(id);
                if (list == null)
                    return Result<ToDoList>.Failure("List not found");
                
                return Result<ToDoList>.Success(list);
            }
            catch (Exception ex)
            {
                return Result<ToDoList>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> AddListAsync(ToDoList list)
        {
            try
            {
                await _repository.AddListAsync(list);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> UpdateListAsync(ToDoList list)
        {
            try
            {
                await _repository.UpdateListAsync(list);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> DeleteListAsync(int id)
        {
            try
            {
                await _repository.DeleteListAsync(id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }
    }
}