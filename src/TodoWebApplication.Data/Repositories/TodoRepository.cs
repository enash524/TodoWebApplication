using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Domain.Models;

namespace TodoWebApplication.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<TodoModel> _todoModels = new List<TodoModel>
        {
            new TodoModel
            {
                Id = 1,
                Title = "todo 1",
                Description = "pick up groceries",
                Complete = true,
                Date = new DateTime(2920, 1, 23),
                Priority = "high"
            },
            new TodoModel
            {
                Id = 2,
                Title = "todo 2",
                Description = "study javascript",
                Complete = false,
                Date = new DateTime(2920, 1, 23),
                Priority = "high"
            },
            new TodoModel
            {
                Id = 3,
                Title = "todo 3",
                Description = "go to gym",
                Complete = false,
                Date = new DateTime(2920, 1, 23),
                Priority = "low"
            },
            new TodoModel
            {
                Id = 4,
                Title = "todo 4",
                Description = "drive to dealership to change oil",
                Complete = false,
                Date = new DateTime(2920, 1, 25),
                Priority = "low"
            },
            new TodoModel
            {
                Id = 5,
                Title = "todo 5",
                Description = "buy new headphones",
                Complete = false,
                Date = new DateTime(2920, 1, 23),
                Priority = "high"
            }
        };

        public Task<TodoModel> CreateTodoModelAsync(TodoModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTodoModelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoModel> GetTodoModelByIdAsync(int id)
        {
            TodoModel model = _todoModels.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(model);
        }

        public Task<List<TodoModel>> GetTodoModelsAsync()
        {
            return Task.FromResult(_todoModels);
        }

        public Task<bool> UpdateTodoModelAsync(int id, TodoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
