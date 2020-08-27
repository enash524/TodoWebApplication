using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Models;
using TodoWebApplication.Repositories;

namespace TodoWebApplication.Controllers
{
    public class TodoController : BaseController
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
            : base()
        {
            _todoRepository = todoRepository;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TodoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(List<TodoModel>), StatusCodes.Status200OK)]
        public ActionResult<List<TodoModel>> GetTodoModels()
        {
            return Ok(_todoRepository.GetTodoModels());
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoModel), StatusCodes.Status200OK)]
        public ActionResult<TodoModel> CreateTodoModel(TodoModel model)
        {
            TodoModel result = _todoRepository.CreateTodoModel(model);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public ActionResult<bool> UpdateTodoModel([FromRoute] int id, TodoModel model)
        {
            bool result = _todoRepository.UpdateTodoModel(id, model);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public ActionResult<bool> DeleteTodoModel([FromRoute] int id)
        {
            bool result = _todoRepository.DeleteTodoModel(id);
            return Ok(result);
        }
    }
}
