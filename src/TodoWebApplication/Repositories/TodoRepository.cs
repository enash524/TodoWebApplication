using System;
using System.Collections.Generic;
using System.Linq;
using TodoWebApplication.Models;

namespace TodoWebApplication.Repositories
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

        public TodoModel GetTodoModelById(int id)
        {
            TodoModel model = _todoModels.FirstOrDefault(m => m.Id == id);
            return model;
        }

        public List<TodoModel> GetTodoModels()
        {
            return _todoModels;
        }

        public bool DeleteTodoModel(int id)
        {
            throw new NotImplementedException();
        }

        public TodoModel CreateTodoModel(TodoModel model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTodoModel(int id, TodoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
