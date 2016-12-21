using Proxy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Domain.Concrete
{
    public class TaskContext: DbContext
    {
        public TaskContext() : base("DefaultConnection")
        {
        }

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
