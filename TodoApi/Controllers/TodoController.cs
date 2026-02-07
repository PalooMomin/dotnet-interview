using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TodoController : ControllerBase
    {
        private readonly IRepositoryTodo _repository;
        public TodoController(IRepositoryTodo repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _repository.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> CreateTodo(Todo Todo)
        {
            await _repository.AddAsync(Todo);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = Todo.Id }, Todo);
        }

        //[HttpPost("createTodo")]
        //public IActionResult CreateTodo([FromBody] Todo todo)
        //{
        //    try
        //    {
        //        var todoService = new TodoService();
        //        var result = todoService.CreateTodo(todo);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            try
            {
                if (id!=0)
                {
                    var todo = await _repository.GetByIdAsync(id);
                    return todo == null ? NotFound() : Ok(todo);
                }
                else
                {
                    return (IActionResult)await _repository.GetAllAsync();
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("getTodo")]
        //public IActionResult GetTodo([FromBody] GetTodoRequest request)
        //{
        //    try
        //    {
        //        var todoService = new TodoService();
        //        if (request.Id.HasValue)
        //        {
        //            var todo = todoService.GetTodoById(request.Id.Value);
        //            if (todo == null)
        //            {
        //                return NotFound();
        //            }
        //            return Ok(todo);
        //        }
        //        else
        //        {
        //            var todos = todoService.GetAllTodos();
        //            return Ok(todos);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo product)
        {
            if (id != product.Id) return BadRequest();

            _repository.Update(product);
            await _repository.SaveAsync();
            return NoContent();
        }

        //[HttpPost("updateTodo")]
        //public IActionResult UpdateTodo([FromBody] UpdateTodoRequest request)
        //{
        //    try
        //    {
        //        var todoService = new TodoService();
        //        var existingTodo = todoService.GetTodoById(request.Id);
        //        if (existingTodo == null)
        //        {
        //            return NotFound();
        //        }

        //        var todo = new Todo
        //        {
        //            Title = request.Title,
        //            Description = request.Description,
        //            IsCompleted = request.IsCompleted
        //        };

        //        var result = todoService.UpdateTodo(request.Id, todo);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            _repository.Delete(product);
            await _repository.SaveAsync();
            return NoContent();
        }


        //[HttpPost("deleteTodo")]
        //public IActionResult DeleteTodo([FromBody] DeleteTodoRequest request)
        //{
        //    try
        //    {
        //        var todoService = new TodoService();
        //        var result = todoService.DeleteTodo(request.Id);
        //        if (result)
        //        {
        //            return Ok(new { message = "Todo deleted successfully" });
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }

    public class GetTodoRequest
    {
        public int? Id { get; set; }
    }

    public class UpdateTodoRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class DeleteTodoRequest
    {
        public int Id { get; set; }
    }
}
