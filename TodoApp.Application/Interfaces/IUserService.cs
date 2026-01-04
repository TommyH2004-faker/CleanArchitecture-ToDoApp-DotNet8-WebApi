using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<List<UserDto>>> GetAllUsersAsync();
        Task<Result<UserDto>> GetUserByIdAsync(int id);
        Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto);
        Task<Result<UserDto>> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<Result> DeleteUserAsync(int id);
    }
}
