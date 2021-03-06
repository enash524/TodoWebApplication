﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }

        public DateTime Date { get; set; }

        public string Priority { get; set; }
    }
}
