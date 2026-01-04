using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Validate command
                var validationErrors = new List<string>();

                if (string.IsNullOrWhiteSpace(command.Username))
                    validationErrors.Add("Username is required");

                if (string.IsNullOrWhiteSpace(command.Email))
                    validationErrors.Add("Email is required");

                if (validationErrors.Any())
                    return Result<UserDto>.Failure(validationErrors);

                // Check if username or email already exists
                var existingUsers = await _userRepository.GetAllUsersAsync();
                if (existingUsers.Any(u => u.Username.Equals(command.Username, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Username already exists");

                if (existingUsers.Any(u => u.Email.Equals(command.Email, StringComparison.OrdinalIgnoreCase)))
                    return Result<UserDto>.Failure("Email already exists");

                // Create entity using factory method
                // Note: This is legacy code - use Auth Register endpoint for new users with passwords
                var user = User.Create(command.Username, command.Email, "LEGACY_USER_NO_PASSWORD");

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
    }
}
