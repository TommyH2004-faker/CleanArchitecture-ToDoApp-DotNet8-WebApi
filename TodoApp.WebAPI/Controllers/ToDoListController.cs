using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] ToDoList toDoList)
        {
            await _toDoListService.AddListAsync(toDoList);
            return CreatedAtAction(nameof(GetToDoList), new { id = toDoList.ToDoListId }, toDoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoList(int id)
        {
            var toDoList = await _toDoListService.GetListByIdAsync(id);
            return toDoList == null ? NotFound() : Ok(toDoList);
        }
    }

}
