using Microsoft.AspNetCore.Mvc;
using Pograminha.Domain.Contracts;
using Pograminha.Domain.Model;
using Pograminha.Model;
using System;
using System.Threading.Tasks;

namespace Pograminha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoService;

        public TodoItemsController(ITodoItemService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var todos = await _todoService.FindAllAsync();

            return Ok(new { data = todos });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.FindOneAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var viewmodel = new TodoItemDTO
            {
                Id = todoItem.Id,
                IsComplete = todoItem.IsComplete,
                Name = todoItem.Name
            };

            return Ok(new { data = viewmodel });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoService.FindOneAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            await _todoService.UpdateAsync(todoItem);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await _todoService.CreateAsync(todoItem);

            return Created(new Uri($"https://localhost:5001/api/todoitems/{todoItem.Id}"), todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoService.FindOneAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoService.DeleteAsync(todoItem);

            return NoContent();
        }
    }
}
