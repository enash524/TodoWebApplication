﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Domain.Models;

namespace TodoWebApplication.Application.Queries.Todo
{
    /// <summary>
    /// Contains methods for handling the GetTodosQuery
    /// </summary>
    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, QueryResult<List<TodoModel>>>
    {
        private readonly ILogger<GetTodosQueryHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTodosQueryHandler`1"/> class.
        /// </summary>
        public GetTodosQueryHandler(
            ILogger<GetTodosQueryHandler> logger,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// Handles the GetTodoByIdQuery
        /// </summary>
        /// <param name="request">The GetTodoByIdQuery input parameters.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task representing the TodoModel wrapped in a QueryResult.</returns>
        public async Task<QueryResult<List<TodoModel>>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            List<TodoModel> entity = await _todoRepository.GetTodoModelsAsync();

            return new QueryResult<List<TodoModel>>
            {
                QueryResultType = QueryResultType.Success,
                Result = entity
            };
        }
    }
}
