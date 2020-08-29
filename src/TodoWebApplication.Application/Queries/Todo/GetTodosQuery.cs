using System.Collections.Generic;
using MediatR;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Domain.Models;

namespace TodoWebApplication.Application.Queries.Todo
{
    public class GetTodosQuery : IRequest<QueryResult<List<TodoModel>>>
    {
    }
}
