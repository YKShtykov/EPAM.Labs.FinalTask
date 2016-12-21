using Proxy.Domain.Abstract;
using Proxy.Domain.Entities;
using Proxy.Web.Infrustructure;
using Proxy.Web.Interfaces;
using Proxy.Web.Models;
using Proxy.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Proxy.Web.Controllers
{
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class TaskController : ApiController
    {
        private readonly IRequestManager _manager;
        private readonly ITaskRepository _repository;
        private readonly ITaskConvertor _convertor;

        private const int uID = 32;

        private readonly UserService userService = new UserService();

        public TaskController(IRequestManager manager, ITaskRepository repository, ITaskConvertor convertor)
        {
            _manager = manager;
            _repository = repository;
            _convertor = convertor;
            //Equalizer.Equalize(_repository, _manager, _convertor, uID);
        }

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<ToDoItemViewModel> Get(int userId)
        {
            //Equalizer.Equalize(_repository, _manager, _convertor, uID);
            var tasks = _repository.UserTasks(userId);
            IList<ToDoItemViewModel> models = _convertor.ConvertToListModel(tasks.ToList());
            return models;
            //return _manager.Get(uID);
            //return _manager.Get(userId);
        }

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(ToDoItemViewModel todo)
        {
            _repository.AddTask(_convertor.ConvertToTask(todo));
            //_manager.Put(todo);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.RemoveTask(id);
            //_manager.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(ToDoItemViewModel todo)
        {
            _repository.AddTask(_convertor.ConvertToTask(todo));
            //_manager.Post(todo);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
