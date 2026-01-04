using MediatR;

namespace TodoApp.Application.Features.Auth.Commands.Refresh
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
