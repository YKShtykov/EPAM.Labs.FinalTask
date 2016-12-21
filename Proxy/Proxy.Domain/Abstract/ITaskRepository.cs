using Proxy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Domain.Abstract
{
    public interface ITaskRepository
    {
        IQueryable<ToDoTask> Tasks { get; }

        IQueryable<User> Users {get;}

        IList<ToDoTask> UserTasks(int userId);

        void RemoveTask(int taskId);

        void AddTask(ToDoTask task);

        void UpdateTask(ToDoTask task);

        IList<ToDoTask> EqualizeTasks(IList<ToDoTask> cloudTasks, int taskId);

        bool ExistUser(int id);
    }
}
