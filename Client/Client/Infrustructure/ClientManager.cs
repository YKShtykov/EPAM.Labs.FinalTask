using Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Client.Models;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;

namespace Client.Infrustructure
{
  public class ClientManager : IRequestManager
  {
    /// <summary>
    /// The service URL.
    /// </summary>
    private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ProxyUrl"];
    private readonly string useridUrl = "http://localhost:51341/api/UserId/";
    /// <summary>
    /// The url for getting all tasks.
    /// </summary>
    private const string GetAllUrl = "Get?userId={0}";

    /// <summary>
    /// The url for updating a tasks.
    /// </summary>
    private const string UpdateUrl = "Put";

    /// <summary>
    /// The url for a tasks creation.
    /// </summary>
    private const string CreateUrl = "Post";

    /// <summary>
    /// The url for a tasks deletion.
    /// </summary>
    private const string DeleteUrl = "Delete/{0}";

    //   private readonly HttpClient httpClient;

    public ClientManager()
    {
      // httpClient = new HttpClient();
      //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    /// Deletes a task.
    /// </summary>
    /// <param name="id">The task Id to delete.</param>
    public void Delete(int taskId)
    {
      try
      {
        using (var httpClient = new HttpClient())
        {
          httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          httpClient.DeleteAsync(string.Format(serviceApiUrl + DeleteUrl, taskId))
              .Result.EnsureSuccessStatusCode();
        }
      }
      catch (Exception ex)
      {

      }
    }

    /// <summary>
    /// Gets all tasks for the user.
    /// </summary>
    /// <param name="userId">The User Id.</param>
    /// <returns>The list of todos.</returns>
    public IList<ToDoItemViewModel> Get(int userId)
    {
      string dataAsString = string.Empty;
      var baseAddress = new Uri(serviceApiUrl + GetAllUrl);
      try
      {
        var cookieContainer = new CookieContainer();
        using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
        {
          using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
          {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            cookieContainer.Add(new Uri(serviceApiUrl + GetAllUrl), new Cookie("user", "100"));
            dataAsString = client.GetStringAsync(serviceApiUrl + GetAllUrl).Result;
            var a = HttpContext.Current.Response.Cookies;
            return JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
          }
        }
        
      }
      catch (Exception ex)
      {

      }
      return null;
    }

    /// <summary>
    /// Creates a task. UserId is taken from the model.
    /// </summary>
    /// <param name="item">The todo to create.</param>
    public void Post(ToDoItemViewModel task)
    {
      try
      {
        using (var httpClient = new HttpClient())
        {
          httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, task)
              .Result.EnsureSuccessStatusCode();
        }
      }
      catch (Exception ex)
      {

      }
    }

    /// <summary>
    /// Updates a todo.
    /// </summary>
    /// <param name="item">The todo to update.</param>
    public void Put(ToDoItemViewModel task)
    {
      try
      {
        using (var httpClient = new HttpClient())
        {
          httpClient.PutAsJsonAsync(serviceApiUrl + UpdateUrl, task)
              .Result.EnsureSuccessStatusCode();
        }
      }
      catch (Exception ex)
      {

      }
    }

    public int GetId()
    {
      string dataAsString = null;
      var baseAddress = new Uri(serviceApiUrl + GetAllUrl);
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        dataAsString = client.GetStringAsync(useridUrl + GetAllUrl).Result;
        var a = HttpContext.Current.Response.Cookies;
        return JsonConvert.DeserializeObject<int>(dataAsString);
      }
    }
  }
}