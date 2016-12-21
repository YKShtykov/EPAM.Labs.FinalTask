using Proxy.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proxy.Domain.Entities;
using Proxy.Web.Models;

namespace Proxy.Web.Infrustructure
{
    public class Convertor : ITaskConvertor
    {
        public ToDoItemViewModel ConvertToModel(ToDoTask task)
        {
            if (task == null) return null;
            ToDoItemViewModel model = new ToDoItemViewModel();
            model.IsCompleted = task.IsCompleted;
            model.Name = task.Name;
            if (task.CloudId!=null)
            {
                model.ToDoId = (int)task.CloudId;
            }
            else
            {
                model.ToDoId = task.Id;
            }            
            model.UserId = task.UserId;
            return model;
        }

        public ToDoTask ConvertToTask(ToDoItemViewModel model)
        {
            if (model == null) return null;
            ToDoTask task = new ToDoTask();
            task.UserId = model.UserId;
            task.Name = model.Name;
            task.Create = true;
            task.CloudId = model.ToDoId;
            task.Id = model.ToDoId;
            return task;
        }

        public IList<ToDoItemViewModel> ConvertToListModel(IList<ToDoTask> tasks)
        {
            if (tasks == null) return null;
            IList<ToDoItemViewModel> models = new List<ToDoItemViewModel>();
            foreach(ToDoTask t in tasks)
            {
                ToDoItemViewModel model = ConvertToModel(t);
                models.Add(model);
            }
            return models;
        }

        public IList<ToDoTask> ConvertToListTask(IList<ToDoItemViewModel> models)
        {
            if (models == null) return null;
            IList<ToDoTask> tasks = new List<ToDoTask>();
            foreach (ToDoItemViewModel m in models)
            {
                ToDoTask task = ConvertToTask(m);
                tasks.Add(task);
            }
            return tasks;
        }
    }
}