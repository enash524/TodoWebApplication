using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Repositories;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet("{id:int}")]
        public ActionResult<TodoModel> GetTodoModelById(int id)
        {
            TodoModel model = _todoRepository.GetTodoModelById(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        public ActionResult<TodoModel> GetTodoModels()
        {
            return Ok(_todoRepository.GetTodoModels());
        }

        [HttpPost]
        public ActionResult<TodoModel> CreateTodoModel(TodoModel model)
        {
            TodoModel result = _todoRepository.CreateTodoModel(model);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<bool> UpdateTodoModel([FromRoute] int id, TodoModel model)
        {
            bool result = _todoRepository.UpdateTodoModel(id, model);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteTodoModel([FromRoute] int id)
        {
            bool result = _todoRepository.DeleteTodoModel(id);
            return Ok(result);
        }
    }
}