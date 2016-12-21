using Proxy.Domain.Abstract;
using Proxy.Domain.Entities;
using Proxy.Web.Interfaces;
using Proxy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proxy.Web.Services
{
    public static class Equalizer
    {
        public static void Equalize(ITaskRepository repository, IRequestManager manager, ITaskConvertor convertor, int userId)
        {
            IList<ToDoTask> cloudTasks = convertor.ConvertToListTask(manager.Get(userId));
            IList<ToDoTask> tasksToUpdate = repository.EqualizeTasks(cloudTasks, userId);
            if (tasksToUpdate != null)
            {
                foreach (ToDoTask t in tasksToUpdate)
                {
                    if (t.Create == true)
                    {
                        t.UserId = userId;
                        manager.Post(convertor.ConvertToModel(t));
                    }
                    else
                    {
                        manager.Delete(convertor.ConvertToModel(t).ToDoId);
                    }
                }
            }
        }
    }
}