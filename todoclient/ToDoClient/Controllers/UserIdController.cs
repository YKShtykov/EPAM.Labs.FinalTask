using System.Collections.Generic;
using System.Web.Http;
using ToDoClient.Models;
using ToDoClient.Services;

namespace todoclient.Controllers
{
  public class UserIdController : ApiController
  {
    private readonly UserService userService = new UserService();
    public int Get()
    {
      return userService.GetOrCreateUser();
    }
  }
}
