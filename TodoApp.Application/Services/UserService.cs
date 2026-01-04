using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var userDtos = users.ToDto();
                return Result<List<UserDto>>.Success(userDtos);
            }
            catch (Exception ex)
            {
                return Result<List<UserDto>>.Failure($"Error retrieving users: {ex.Message}");
            }
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return Result<UserDto>.Failure("Invalid user ID");

                var user = await _userRepository.GetUserByIdAsync(id);
                
                if (user == null)
                    return Result<UserDto>.Failure("User not found");

                return Result<UserDto>.Success(user.ToDto());
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Failure($"Error retrieving user: {ex.Message}");
            }
        }

        public async Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                // Validate DTO
                var validationErrors = new List<string>();
                
                if (string.IsNullOrWhiteSpace(dto.Username))
                    validationErrors.Add("Username is required");
                
                if (string.IsNullOrWhiteSpace(dto.Email))
                    validationErrors.Add("Email is required");

                if (validationErrors.Any())
                    return Result<UserDto>.Failure(validationErrors);

                // Check if username or email already exists
                var existingUsers = await _userRepository.GetAllUsersAsync();
                if (existingUsers.Any(u => u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Username already exists");

                if (existingUsers.Any(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Email already exists");

                // Create entity using factory method (với validation)
                // Note: This is legacy code - use Auth Register endpoint for new users with passwords
                var user = User.Create(dto.Username, dto.Email, "LEGACY_USER_NO_PASSWORD");

                // Save to database
                await _userRepository.AddUserAsync(user);

                return Result<UserDto>.Success(user.ToDto());
            }
            catch (ArgumentException ex)
            {
                return Result<UserDto>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Failure($"Error creating user: {ex.Message}");
            }
        }

        public async Task<Result<UserDto>> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            try
            {
                if (id <= 0)
                    return Result<UserDto>.Failure("Invalid user ID");

                var user = await _userRepository.GetUserByIdAsync(id);
                
                if (user == null)
                    return Result<UserDto>.Failure("User not found");

                // Validate DTO
                var validationErrors = new List<string>();
                
                if (string.IsNullOrWhiteSpace(dto.Username))
                    validationErrors.Add("Username is required");
                
                if (string.IsNullOrWhiteSpace(dto.Email))
                    validationErrors.Add("Email is required");

                if (validationErrors.Any())
                    return Result<UserDto>.Failure(validationErrors);

                // Check if username or email already exists (excluding current user)
                var existingUsers = await _userRepository.GetAllUsersAsync();
                if (existingUsers.Any(u => u.UserId != id && u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Username already exists");

                if (existingUsers.Any(u => u.UserId != id && u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Email already exists");

                // Update using domain method (với validation)
                user.Update(dto.Username, dto.Email);

                await _userRepository.UpdateUserAsync(user);

                return Result<UserDto>.Success(user.ToDto());
            }
            catch (ArgumentException ex)
            {
                return Result<UserDto>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Failure($"Error updating user: {ex.Message}");
            }
        }

        public async Task<Result> DeleteUserAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return Result.Failure("Invalid user ID");

                var user = await _userRepository.GetUserByIdAsync(id);
                
                if (user == null)
                    return Result.Failure("User not found");

                await _userRepository.DeleteUserAsync(id);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting user: {ex.Message}");
            }
        }
    }
}
