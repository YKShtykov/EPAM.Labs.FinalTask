using Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Client.Infrustructure
{
  public class UserService : IUserService
  {
    private readonly string useridUrl = "http://localhost:51341/api/UserId/";
    private const string GetAllUrl = "Get?userId={0}";

    public int CreateUser()
    {
    string dataAsString = null;
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        dataAsString = client.GetStringAsync(useridUrl + GetAllUrl).Result;
        var a = HttpContext.Current.Response.Cookies;
        return JsonConvert.DeserializeObject<int>(dataAsString);
      }
    }

    public int GetCurrentUser()
    {
      var userCookie = HttpContext.Current.Request.Cookies["user"];
      int userId;
      if (userCookie == null || !int.TryParse(userCookie.Value, out userId))
        return -1;

      return userId;
      }

    public bool LogIn(int UserId)
    {
      return true;
    }

    public void Logout()
    {
      var userCookie = HttpContext.Current.Request.Cookies["user"];
      int userId;
      if (userCookie == null || !int.TryParse(userCookie.Value, out userId))
        return;

      if (HttpContext.Current.Request.Cookies["user"] != null)
      {
        var c = new HttpCookie("user");
        c.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(c);
      }
    }
  }
}