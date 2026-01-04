using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(int UserId, string Username, string Email) 
        : IRequest<Result<UserDto>>;
}
