using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Repositories
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
