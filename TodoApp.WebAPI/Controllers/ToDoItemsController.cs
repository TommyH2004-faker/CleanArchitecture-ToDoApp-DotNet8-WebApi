using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Features.TodoItem.Commands.CreateTodoItem;
using TodoApp.Application.Features.TodoItem.Commands.Update;
using TodoApp.Application.Features.TodoItem.Commands.Delete;
using TodoApp.Application.Features.TodoItem.Queries.GetToDoItemById;
using TodoApp.Application.Features.TodoItem.Queries.GetAllToDoItems;
using static TodoApp.Application.DTOs.ToDoItemDto;

namespace TodoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllToDoItemsQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetToDoItemByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateToDoItemDto dto)
        {
            var command = new CreateToDoItemCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.ToDoItemId }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateToDoItemDto dto)
        {
            var command = new UpdateToDoItemCommand(id, dto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteToDoItemCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(result.Errors);
            // báo xoá thành công
            return Ok(new { message = "ToDoItem deleted successfully" });
        }
    }
}