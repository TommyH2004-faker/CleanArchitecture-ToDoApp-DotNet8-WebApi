using MediatR;
using TodoApp.Application.Interfaces;
using BCrypt.Net;

namespace TodoApp.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Verify password with BCrypt
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            var tokenExpiration = _jwtService.GetTokenExpiration();

            // Generate refresh token
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenExpiry = _jwtService.GetRefreshTokenExpiration();

            // Save refresh token to user
            user.SetRefreshToken(refreshToken, refreshTokenExpiry);
            await _userRepository.UpdateUserAsync(user);

            return new LoginResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = tokenExpiration,
                Role = user.Role
            };
        }
    }
}
