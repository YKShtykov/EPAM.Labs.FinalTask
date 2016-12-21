using Proxy.Domain.Entities;
using Proxy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proxy.Web.Interfaces
{
    public interface ITaskConvertor
    {
        ToDoTask ConvertToTask(ToDoItemViewModel model);

        ToDoItemViewModel ConvertToModel(ToDoTask task);

        IList<ToDoItemViewModel> ConvertToListModel(IList<ToDoTask> tasks);

        IList<ToDoTask> ConvertToListTask(IList<ToDoItemViewModel> models);
    }
}