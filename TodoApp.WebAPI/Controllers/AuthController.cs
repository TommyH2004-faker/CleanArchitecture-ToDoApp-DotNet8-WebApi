using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Auth.Commands.Login;
using TodoApp.Application.Features.Auth.Commands.Register;
using TodoApp.Application.Features.Auth.Commands.Refresh;

namespace TodoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                // Set refresh token vào HttpOnly cookie
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.ExpiresAt.AddHours(9) // refresh token 10h
                });
                // Trả thêm trường role về body
                return Ok(new {
                    userId = result.UserId,
                    username = result.Username,
                    email = result.Email,
                    token = result.Token,
                    role = result.Role, // thêm dòng này
                    expiresAt = result.ExpiresAt
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Refresh access token using refresh token from HttpOnly cookie
        /// </summary>
        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshTokenResponse>> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "No refresh token" });

            var command = new RefreshTokenCommand { RefreshToken = refreshToken };
            try
            {
                var result = await _mediator.Send(command);
                // Set refresh token mới vào cookie
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.ExpiresAt.AddHours(9) // refresh token 10h
                });
                return Ok(new { token = result.Token, expiresAt = result.ExpiresAt });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
