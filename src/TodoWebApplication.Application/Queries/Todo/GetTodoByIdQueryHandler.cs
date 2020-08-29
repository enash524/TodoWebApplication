using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Domain.Models;

namespace TodoWebApplication.Application.Queries.Todo
{
    /// <summary>
    /// Contains methods for handling the GetTodoByIdQuery
    /// </summary>
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, QueryResult<TodoModel>>
    {
        private readonly ILogger<GetTodoByIdQueryHandler> _logger;
        private readonly ITodoRepository _todoRepository;
        private readonly IValidator<GetTodoByIdQuery> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTodoByIdQueryHandler`1"/> class.
        /// </summary>
        public GetTodoByIdQueryHandler(
            ILogger<GetTodoByIdQueryHandler> logger,
            ITodoRepository todoRepository,
            IValidator<GetTodoByIdQuery> validator)
        {
            _logger = logger;
            _todoRepository = todoRepository;
            _validator = validator;
        }

        /// <summary>
        /// Handles the GetTodoByIdQuery
        /// </summary>
        /// <param name="request">The GetTodoByIdQuery input parameters.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task representing the TodoModel wrapped in a QueryResult.</returns>
        public async Task<QueryResult<TodoModel>> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                _logger.LogError("GetTodoByIdQuery with Id: {id} produced errors on validation {errors}", request.Id, validationResult.ToString());

                return new QueryResult<TodoModel>
                {
                    QueryResultType = QueryResultType.Invalid,
                    Result = null
                };
            }

            TodoModel entity = await _todoRepository.GetTodoModelByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogError("GetTodoByIdQuery with Id: {id} was not found.", request.Id);

                return new QueryResult<TodoModel>
                {
                    QueryResultType = QueryResultType.NotFound,
                    Result = null
                };
            }

            return new QueryResult<TodoModel>
            {
                QueryResultType = QueryResultType.Success,
                Result = entity
            };
        }
    }
}
