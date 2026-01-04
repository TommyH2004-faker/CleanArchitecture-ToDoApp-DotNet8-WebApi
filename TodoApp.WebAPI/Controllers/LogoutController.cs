using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Features.Auth.Commands.Login;
using TodoApp.Application.Features.Auth.Commands.Register;
using TodoApp.Application.Features.Auth.Commands.Refresh;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace TodoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public LogoutController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Lấy userId từ JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (userIdClaim == null) return Unauthorized();
            if (!int.TryParse(userIdClaim.Value, out var userId)) return Unauthorized();
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return Unauthorized();
            user.ClearRefreshToken();
            await _userRepository.UpdateUserAsync(user);
            // Xóa cookie
            Response.Cookies.Delete("refreshToken");
            return Ok(new { message = "Logged out and refresh token revoked" });
        }
    }
}
