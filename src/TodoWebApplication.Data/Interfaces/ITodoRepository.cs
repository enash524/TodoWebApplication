using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApplication.Domain.Models;

namespace TodoWebApplication.Data.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoModel> CreateTodoModelAsync(TodoModel model);

        Task<bool> DeleteTodoModelAsync(int id);

        Task<TodoModel> GetTodoModelByIdAsync(int id);

        Task<List<TodoModel>> GetTodoModelsAsync();

        Task<bool> UpdateTodoModelAsync(int id, TodoModel model);
    }
}
