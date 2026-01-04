using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;

namespace TodoApp.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                if (query.UserId <= 0)
                    return Result<UserDto>.Failure("Invalid user ID");

                var user = await _userRepository.GetUserByIdAsync(query.UserId);

                if (user == null)
                    return Result<UserDto>.Failure("User not found");

                return Result<UserDto>.Success(user.ToDto());
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Failure($"Error retrieving user: {ex.Message}");
            }
        }
    }
}
