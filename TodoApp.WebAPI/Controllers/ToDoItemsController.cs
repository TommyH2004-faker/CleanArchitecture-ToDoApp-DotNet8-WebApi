using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemController(IToDoItemService toDoItemService)
        {
            this._toDoItemService = toDoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
        {
            var response = await _toDoItemService.GetAllItemsAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetById(int id)
        {
            var item = await _toDoItemService.GetItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ToDoItem item)
        {
            await _toDoItemService.AddItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.ToDoItemId }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ToDoItem item)
        {
            if (id != item.ToDoItemId) return BadRequest();
            await _toDoItemService.UpdateItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _toDoItemService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
