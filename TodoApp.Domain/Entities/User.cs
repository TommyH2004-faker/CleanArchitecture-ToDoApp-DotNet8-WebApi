using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Entities
{
    public class User
    {
        // Private constructor để force sử dụng factory method
        private User() 
        {
            ToDoLists = new List<ToDoList>();
        }

        // EF Core cần set được UserId khi insert
        public int UserId { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
            public string Role { get; private set; } = "User";
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiry { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<ToDoList> ToDoLists { get; private set; } // a user can have multiple todo list!

        // Factory method để tạo User với validation
        public static User Create(string username, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty", nameof(passwordHash));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format", nameof(email));

            if (username.Length < 3 || username.Length > 50)
                throw new ArgumentException("Username must be between 3 and 50 characters", nameof(username));

            return new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                Role = "User",
                CreatedAt = DateTime.UtcNow
                // UserId sẽ được EF Core set sau khi SaveChanges
            };
        }

        // Method để update User
        public void Update(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format", nameof(email));

            if (username.Length < 3 || username.Length > 50)
                throw new ArgumentException("Username must be between 3 and 50 characters", nameof(username));

            Username = username;
            Email = email;
        }

        // Method để update password
        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Password hash cannot be empty", nameof(newPasswordHash));

            PasswordHash = newPasswordHash;
        }

        // Method để update refresh token
        public void SetRefreshToken(string refreshToken, DateTime expiry)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("Refresh token cannot be empty", nameof(refreshToken));

            if (expiry <= DateTime.UtcNow)
                throw new ArgumentException("Expiry must be in the future", nameof(expiry));

            RefreshToken = refreshToken;
            RefreshTokenExpiry = expiry;
        }

        // Method để clear refresh token (khi logout)
        public void ClearRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiry = null;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
