using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mappings;

namespace TodoApp.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
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
    }
}
