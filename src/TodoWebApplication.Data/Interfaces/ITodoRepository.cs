using System.Collections.Generic;
using TodoWebApplication.Models;

namespace TodoWebApplication.Repositories
{
    public interface ITodoRepository
    {
        TodoModel GetTodoModelById(int id);

        List<TodoModel> GetTodoModels();

        bool DeleteTodoModel(int id);

        TodoModel CreateTodoModel(TodoModel model);

        bool UpdateTodoModel(int id, TodoModel model);
    }
}
