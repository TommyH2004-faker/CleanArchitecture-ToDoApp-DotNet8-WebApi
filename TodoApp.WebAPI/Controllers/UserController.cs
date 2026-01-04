using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Users.Commands.CreateUser;
using TodoApp.Application.Features.Users.Commands.UpdateUser;
using TodoApp.Application.Features.Users.Commands.DeleteUser;
using TodoApp.Application.Features.Users.Queries.GetAllUsers;
using TodoApp.Application.Features.Users.Queries.GetUserById;

namespace TodoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            
            if (!result.IsSuccess)
                return BadRequest(new { error = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            
            if (!result.IsSuccess)
                return NotFound(new { error = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
            {
                if (result.Errors.Any())
                    return BadRequest(new { errors = result.Errors });
                
                return BadRequest(new { error = result.ErrorMessage });
            }

            return CreatedAtAction(nameof(GetUser), new { id = result.Data!.UserId }, result.Data);
        }

        /// <summary>
        /// Update user
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Override ID from route
            var updatedCommand = command with { UserId = id };
            var result = await _mediator.Send(updatedCommand);
            
            if (!result.IsSuccess)
            {
                if (result.Errors.Any())
                    return BadRequest(new { errors = result.Errors });
                
                return BadRequest(new { error = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            
            if (!result.IsSuccess)
                return NotFound(new { error = result.ErrorMessage });

            return NoContent();
        }
    }
}
