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
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        IList<ToDoItemViewModel> Get(int userId);

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        void Put(ToDoItemViewModel task);

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
