using MediatR;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using BCrypt.Net;

namespace TodoApp.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already registered");
            }

            // Check if username already exists
            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUsername != null)
            {
                throw new InvalidOperationException("Username already taken");
            }

            // Hash password with BCrypt (WorkFactor 12 is standard)
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 12);

            // Create user entity
            var user = User.Create(request.Username, request.Email, passwordHash);

            // Save to database
            await _userRepository.AddAsync(user);

            return new RegisterResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
