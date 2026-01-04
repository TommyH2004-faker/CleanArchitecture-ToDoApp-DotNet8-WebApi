using MediatR;
using TodoApp.Application.Common;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int UserId) : IRequest<Result<UserDto>>;
}
