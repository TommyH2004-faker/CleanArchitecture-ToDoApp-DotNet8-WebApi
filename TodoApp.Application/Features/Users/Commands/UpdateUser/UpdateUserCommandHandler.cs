using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;

namespace TodoApp.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.UserId <= 0)
                    return Result<UserDto>.Failure("Invalid user ID");

                var user = await _userRepository.GetUserByIdAsync(command.UserId);

                if (user == null)
                    return Result<UserDto>.Failure("User not found");

                // Check if username or email already exists (excluding current user)
                var existingUsers = await _userRepository.GetAllUsersAsync();
                if (existingUsers.Any(u => u.UserId != command.UserId && 
                    u.Username.Equals(command.Username, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Username already exists");

                if (existingUsers.Any(u => u.UserId != command.UserId && 
                    u.Email.Equals(command.Email, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Email already exists");

                // Update using domain method
                user.Update(command.Username, command.Email);

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
    }
}
