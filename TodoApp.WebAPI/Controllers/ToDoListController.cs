using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.TodoList.Commands.CreateList;
using TodoApp.Application.Features.TodoList.Commands.DeleteList;
using TodoApp.Application.Features.TodoList.Commands.UpdateList;
using TodoApp.Application.Features.TodoList.Queries.GetAllToDolist;

namespace TodoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoItemListCommand command)
        {
        
            var result = await _mediator.Send(command);

            
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetToDoList), new { toDoListId = result.Data.ToDoListId }, result.Data);
        }

        [HttpGet("{toDoListId}")]
        public async Task<IActionResult> GetToDoList(int toDoListId)
        {
            var query = new GetbyId(toDoListId);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllToDoLists()
        {
            var query = new GetAllList();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(int id)
        {
            var command = new DeleteToDoItemListCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(new { message = "ToDoList deleted successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoList(int id, [FromBody] UpdateCommand command)
        {
            if (id != command.dto.ToDoListId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}