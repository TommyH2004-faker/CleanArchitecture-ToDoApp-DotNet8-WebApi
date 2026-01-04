namespace TodoApp.Application.Features.Auth.Commands.Register
{
    public class RegisterResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = "Registration successful. Please login to get your token.";
    }
}
