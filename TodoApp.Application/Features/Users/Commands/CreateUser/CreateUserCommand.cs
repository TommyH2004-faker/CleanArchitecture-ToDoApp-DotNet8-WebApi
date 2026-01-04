using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(string Username, string Email) 
        : IRequest<Result<UserDto>>;
}
