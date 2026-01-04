using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.UserId <= 0)
                    return Result.Failure("Invalid user ID");

                var user = await _userRepository.GetUserByIdAsync(command.UserId);

                if (user == null)
                    return Result.Failure("User not found");

                await _userRepository.DeleteUserAsync(command.UserId);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error deleting user: {ex.Message}");
            }
        }
    }
}
