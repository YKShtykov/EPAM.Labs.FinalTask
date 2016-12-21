using Proxy.Domain.Abstract;
using Proxy.Domain.Concrete;
using Proxy.Web.Infrustructure;
using Proxy.Web.Interfaces;
using Proxy.Web.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proxy.Web.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private readonly IRequestManager _manager;
        private readonly ITaskRepository _repository;
        private readonly ITaskConvertor _convertor;
        private readonly IUserService _service;

        public UserController(IRequestManager manager, ITaskRepository repository, ITaskConvertor convertor, IUserService service)
        {
            _manager = manager;
            _repository = repository;
            _convertor = convertor;
            _service = service;
        }

        [HttpGet]
        [ActionName("Logout")]
        public void Logout(int userId)
        {
           // Equalizer.Equalize(_repository, _manager, _convertor, userId);
        }

        [HttpGet]
        [ActionName("Login")]
        public bool Login(int userId)
        {
            return _repository.ExistUser(userId);
        }

        [HttpGet]
        [ActionName("CreateUser")]
        public int CreateUser(string userName)
        {
            return 32;
            return _service.CreateUser(userName);
        }
    }
}
