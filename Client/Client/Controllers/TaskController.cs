using Client.Interfaces;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Client.Controllers
{
    /// <summary>
    /// Processes task requests.
    /// </summary>
    public class TaskController : ApiController
    {
        private readonly IRequestManager _RequestManager;

        //private readonly UserService userService = new UserService();

        private const int userId = 32;

        public TaskController (IRequestManager manager)
        {
            _RequestManager = manager;
        }

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<ToDoItemViewModel> Get()
        {
           // var userId = userService.GetOrCreateUser();
            return _RequestManager.Get(userId);
        }

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        public void Put(ToDoItemViewModel task)
        {
            //task.UserId = userService.GetOrCreateUser();
            _RequestManager.Put(task);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        public void Delete(int id)
        {
            _RequestManager.Delete(id);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        public void Post(ToDoItemViewModel task)
        {
            //todo.UserId = userService.GetOrCreateUser();
            _RequestManager.Post(task);
        }
    }
}
