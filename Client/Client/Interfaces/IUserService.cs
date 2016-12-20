using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Interfaces
{
  public interface IUserService
  {
    void Logout();
    bool LogIn(int UserId);
    int CreateUser();
    int GetCurrentUser();
  }
}