using MediatR;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.Auth.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Tìm user theo refresh token
            var users = await _userRepository.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.RefreshToken == request.RefreshToken);
            if (user == null || user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            // Rotation: sinh refresh token mới
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            var newRefreshTokenExpiry = _jwtService.GetRefreshTokenExpiration();
            user.SetRefreshToken(newRefreshToken, newRefreshTokenExpiry);
            await _userRepository.UpdateUserAsync(user);

            // Sinh access token mới
            var token = _jwtService.GenerateToken(user);
            var tokenExpiration = _jwtService.GetTokenExpiration();

            return new RefreshTokenResponse
            {
                Token = token,
                RefreshToken = newRefreshToken,
                ExpiresAt = tokenExpiration
            };
        }
    }
}
