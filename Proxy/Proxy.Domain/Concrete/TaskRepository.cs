using Proxy.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy.Domain.Entities;
using System.Threading;

namespace Proxy.Domain.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private ReaderWriterLockSlim _taskLock = new ReaderWriterLockSlim();
        //private ReaderWriterLockSlim _userLock = new ReaderWriterLockSlim();

        private TaskContext context = new TaskContext();

        public IQueryable<ToDoTask> Tasks
        {
            get
            {
                try
                {
                    _taskLock.EnterReadLock(); 
                    return context.Tasks;
                }
                finally
                {
                    _taskLock.ExitReadLock();
                }
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                return context.Users;
            }
        }

        public void AddTask(ToDoTask task)
        {
            task.Create = true;
            try
            {
                _taskLock.EnterWriteLock();
                context.Tasks.Add(task);
                context.SaveChanges();
            }
            finally
            {
                _taskLock.ExitWriteLock();
            }                        
        }

        public void RemoveTask(int taskId)
        {
            try
            {
                _taskLock.EnterWriteLock();
                ToDoTask task = context.Tasks.Where(t => t.CloudId == taskId).FirstOrDefault();
                if (task == null)
                {
                    task = context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
                }
                if (task != null)
                {                    
                    context.Tasks.Remove(task);
                    context.SaveChanges();
                    task.Create = false;
                    context.Tasks.Add(task);
                    context.SaveChanges();
                }
            }
            finally
            {
                _taskLock.ExitWriteLock();
            }
        }

        public void UpdateTask(ToDoTask task)
        {            
            try
            {
                _taskLock.EnterWriteLock();
                ToDoTask oldTask = context.Tasks.Where(t => t.Id == task.Id).FirstOrDefault();
                if (oldTask != null)
                {
                    if (oldTask.CloudId != null)
                    {
                        task.CloudId = oldTask.CloudId;
                    }
                    context.Tasks.Remove(oldTask);
                    context.SaveChanges();                    
                    context.Tasks.Add(task);
                    context.SaveChanges();
                }
            }
            finally
            {
                _taskLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Creates new user with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if there was no user with such id in context, otherwise false</returns>
        public bool AddUser(int id)
        {
            User user = context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user != null) return false;
            else
            {
                User newUser = new User();
                newUser.UserId = id;
                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
        }

        private IList<ToDoTask> ZeroCloudTasks(int userId)
        {
            IList<ToDoTask> taskToRequest = new List<ToDoTask>();
            List<ToDoTask> baseTasks = context.Tasks.Where(t => t.UserId == userId).ToList();
            var baseTaskToRemove = baseTasks.Where(t => t.Create = false).ToList();
            if (baseTaskToRemove != null)
            {
                foreach (ToDoTask t in baseTaskToRemove)
                {
                    RemoveTask(t.Id);
                }
            }
            var baseTaskToUpdate = baseTasks.Where(t => t.Create = true).ToList();
            {
                foreach (ToDoTask t in baseTaskToUpdate)
                {
                    taskToRequest.Add(t);
                }
            }
            return taskToRequest;
        }

        private void ZeroBaseTasks(IList<ToDoTask> cloudTasks, int userId)
        {
            foreach (ToDoTask cloudTask in cloudTasks)
            {
                ToDoTask newTask = new ToDoTask();
                newTask.UserId = cloudTask.UserId;
                newTask.CloudId = cloudTask.Id;
                newTask.Name = cloudTask.Name;
                AddTask(newTask);
            }
        }

        public IList<ToDoTask> EqualizeTasks(IList<ToDoTask> cloudTasks, int userId)
        {
            IList<ToDoTask> taskToRequest = new List<ToDoTask>();
            List<ToDoTask> baseTasks = context.Tasks.Where(t => t.UserId == userId).ToList();

            if (cloudTasks.Count == 0) return ZeroCloudTasks(userId);
            if (baseTasks.Count == 0) return taskToRequest;

            foreach (ToDoTask cloudTask in cloudTasks)
            {
                var baseTask = baseTasks.Where(t => t.Name == cloudTask.Name).Where(t => t.IsCompleted == cloudTask.IsCompleted).FirstOrDefault();
                if (baseTask == null)
                {
                    ToDoTask newTask = new ToDoTask();
                    newTask.UserId = cloudTask.UserId;
                    newTask.CloudId = cloudTask.Id;
                    newTask.Name = cloudTask.Name;
                    AddTask(newTask);
                }
                else
                {
                    if (baseTask.Create == false)
                    {
                        taskToRequest.Add(baseTask);                        
                    }
                }
            }

            foreach (ToDoTask baseTask in baseTasks)
            {
                var cloudTask = cloudTasks.Where(t => t.Name == baseTask.Name).Where(t => t.IsCompleted == baseTask.IsCompleted).FirstOrDefault();
                if (cloudTask == null) 
                {
                    if (baseTask.Create == true)
                    {
                        taskToRequest.Add(baseTask);
                    }
                    else
                    {
                        RemoveTask(baseTask.Id);
                    }
                }
                else
                {
                    if (baseTask.Create == false)
                    {
                        taskToRequest.Add(baseTask);
                    }
                }
            }
            return taskToRequest;
        }

        public IList<ToDoTask> UserTasks(int userId)
        {
            var userTasks = context.Tasks.Where(t => t.UserId == userId).Where(t => t.Create == true);
            return userTasks.ToList();
        }

        private string TaskAsString(ToDoTask task)
        {
            return task.UserId.ToString() + task.Name.ToString() + task.IsCompleted.ToString();
        }

        public bool ExistUser(int id)
        {
            if (Users.Where(u => u.UserId == id).FirstOrDefault() == null) return false;
            else return true;
        }
    }
}
