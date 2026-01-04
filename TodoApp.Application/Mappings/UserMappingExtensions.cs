using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Mappings
{
    // Extension methods để map giữa Entity và DTO
    public static class UserMappingExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public static List<UserDto> ToDto(this IEnumerable<User> users)
        {
            return users.Select(u => u.ToDto()).ToList();
        }
    }
}
