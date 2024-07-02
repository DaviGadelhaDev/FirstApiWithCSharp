using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")] //indicando a rota
        public IActionResult Get( [FromServices] AppDbContext context)
            => Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetByid( [FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id); 
            if(todo == null)
                return NotFound();
            
            return Ok(todo);
        }
         
        [HttpPost("/")]
        public IActionResult Post( [FromServices] AppDbContext context, [FromBody] TodoModel todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            
            return Created($"/{todo.Id}",todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put( [FromServices] AppDbContext context, TodoModel todo ,[FromBody]  int id)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null)
                return NotFound();
            
            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Put( [FromServices] AppDbContext context, [FromBody]  int id)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();

            return Ok(model);
        }
    }
}