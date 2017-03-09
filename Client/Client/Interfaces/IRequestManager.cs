using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    /// <summary>
    /// Defines interaction between client/proxy and proxy/cloud
    /// </summary>
    public interface IRequestManager
    {        
        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        void Delete(int taskId);

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        void Post(ToDoItemViewModel todo);        
    }
}
