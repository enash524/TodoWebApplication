using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Application.Queries.Todo;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Domain.Models;

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoModel>> GetTodoModelById(int id)
        {
            GetTodoByIdQuery query = new GetTodoByIdQuery
            {
                Id = id
            };

            QueryResult<TodoModel> entity = await Mediator.Send(query);

            if (entity.QueryResultType == QueryResultType.Invalid)
            {
                return BadRequest();
            }

            if (entity.QueryResultType == QueryResultType.NotFound)
            {
                return NotFound();
            }

            return Ok(entity.Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TodoModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoModel>>> GetTodoModels()
        {
            GetTodosQuery query = new GetTodosQuery();
            QueryResult<List<TodoModel>> entity = await Mediator.Send(query);

            return Ok(entity.Result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoModel>> CreateTodoModel(TodoModel model)
        {
            // TODO - USE MediatR!!!
            TodoModel result = await _todoRepository.CreateTodoModelAsync(model);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateTodoModel([FromRoute] int id, TodoModel model)
        {
            // TODO - USE MediatR!!!
            bool result = await _todoRepository.UpdateTodoModelAsync(id, model);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteTodoModel([FromRoute] int id)
        {
            // TODO - USE MediatR!!!
            bool result = await _todoRepository.DeleteTodoModelAsync(id);
            return Ok(result);
        }
    }
}
