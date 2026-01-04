using MediatR;
using TodoApp.Application.Common;

namespace TodoApp.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int UserId) : IRequest<Result>;
}
