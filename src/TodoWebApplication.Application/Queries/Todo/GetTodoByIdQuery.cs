using MediatR;
using TodoWebApplication.Application.Models;
using TodoWebApplication.Domain.Models;

namespace TodoWebApplication.Application.Queries.Todo
{
    public class GetTodoByIdQuery : IRequest<QueryResult<TodoModel>>
    {
        /// <summary>
        /// The todo entitiy ID
        /// </summary>
        public int Id { get; set; }
    }
}
