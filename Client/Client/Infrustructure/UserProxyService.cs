using Client.Interfaces;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Client.Infrustructure
{
  public class UserProxyService : IUserService
  {
    private readonly HttpClient httpClient;
    private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];
    private const string CreateUrl = "Users";
    public UserProxyService()
    {
      httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public int CreateUser()
    {
      var response = httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, "Noname: " + Guid.NewGuid()).Result;
      response.EnsureSuccessStatusCode();
      int userId = response.Content.ReadAsAsync<int>().Result;
      return userId;
    }

    public int GetCurrentUser()
    {
      throw new NotImplementedException();
    }

    public bool LogIn(int UserId)
    {
      throw new NotImplementedException();
    }

    public void Logout()
    {
      throw new NotImplementedException();
    }
  }
}