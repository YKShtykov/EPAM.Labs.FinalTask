using Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Client.Services
{
  public class ClientUserService : IUserService
  {
    private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ProxyUrl"];

    private const string CreateUrl = "User/CreateUser";

    private const string LogoutUrl = "User/Logout";

    private const string LoginUrl = "User/Login";

    private const string CreateUrl1 = "Users";
    private readonly HttpClient httpClient;

    public ClientUserService()
    {
      httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public int CreateUser(string name)
    {
      var response = httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl1, name).Result;
      response.EnsureSuccessStatusCode();
      return response.Content.ReadAsAsync<int>().Result;
      //string userName = ("Noname: " + Guid.NewGuid());
      //using (var httpClient = new HttpClient())
      //{
      //  try
      //  {
      //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      //    string reqStr = serviceApiUrl + CreateUrl + "?userName=" + userName;
      //    var response = httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, userName).Result;
      //    response.EnsureSuccessStatusCode();
      //    int result = response.Content.ReadAsAsync<int>().Result;
      //    var c = new HttpCookie("user", result.ToString());
      //    c.Expires = DateTime.Now.AddDays(1);
      //    HttpContext.Current.Response.Cookies.Add(c);
      //  }
      //  catch (Exception ex)
      //  {

      //  }
      //  return -1;
      //}
    }

    public int GetCurrentUser()
    {
      var userCookie = HttpContext.Current.Request.Cookies["user"];
      int userId;
      if (userCookie == null || !int.TryParse(userCookie.Value, out userId))
        return -1;

      return userId;
    }

    public void Logout(int userId)
    {
      var userCookie = HttpContext.Current.Request.Cookies["user"];
      if (userCookie == null || !int.TryParse(userCookie.Value, out userId))
        return;
      var c = new HttpCookie("user");
      c.Expires = DateTime.Now.AddDays(-1);
      HttpContext.Current.Response.Cookies.Add(c);

      using (var httpClient = new HttpClient())
      {
        try
        {
          string reqStr = serviceApiUrl + LogoutUrl + "?userId=" + userId;
          httpClient.PostAsJsonAsync(reqStr, userId)
              .Result.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {

        }
      }
    }

    public bool Login(int userId)
    {
      string dataAsString = string.Empty;
      try
      {
        using (var httpClient = new HttpClient())
        {
          httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          string reqStr = serviceApiUrl + LoginUrl + "?userId=" + userId;
          dataAsString = httpClient.GetStringAsync(reqStr).Result;
          return JsonConvert.DeserializeObject<bool>(dataAsString);
        }
      }
      catch (Exception ex)
      {

      }
      return false;
    }
    public int GetOrCreateUser()
    {
      var userCookie = HttpContext.Current.Request.Cookies["user"];
      int userId;

      // No user cookie or it's damaged
      if (userCookie == null || !Int32.TryParse(userCookie.Value, out userId))
      {
        userId = CreateUser("Noname: " + Guid.NewGuid());

        // Store the user in a cookie for later access
        var cookie = new HttpCookie("user", userId.ToString())
        {
          Expires = DateTime.Today.AddMonths(1)
        };

        HttpContext.Current.Response.SetCookie(cookie);
      }

      return userId;
    }
  }
}