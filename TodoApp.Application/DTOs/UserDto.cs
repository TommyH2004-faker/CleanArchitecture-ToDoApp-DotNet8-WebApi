namespace TodoApp.Application.DTOs
{
    // DTO để trả về thông tin User (Read)
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // DTO để tạo User mới (Create)
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    // DTO để cập nhật User (Update)
    public class UpdateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class DeleteUserDto
    {
        public int UserId { get; set; }
    }
}
