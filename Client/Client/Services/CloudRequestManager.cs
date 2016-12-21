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

namespace Client.Services
{
  public class CloudRequestManager: IRequestManager
  {/// <summary>
   /// The service URL.
   /// </summary>
    private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ProxyUrl"];

    /// <summary>
    /// The url for getting all tasks.
    /// </summary>
    private const string GetAllUrl = "ToDos?userId={0}";

    /// <summary>
    /// The url for updating a tasks.
    /// </summary>
    private const string UpdateUrl = "ToDos";

    /// <summary>
    /// The url for a tasks creation.
    /// </summary>
    private const string CreateUrl = "ToDos";

    /// <summary>
    /// The url for a tasks deletion.
    /// </summary>
    private const string DeleteUrl = "ToDos/{0}";

    private readonly HttpClient httpClient;
    public CloudRequestManager()
    {
      httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    /// Deletes a task.
    /// </summary>
    /// <param name="id">The task Id to delete.</param>
    public void Delete(int taskId)
    {
      httpClient.DeleteAsync(string.Format(serviceApiUrl + DeleteUrl, taskId))
                .Result.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Gets all tasks for the user.
    /// </summary>
    /// <param name="userId">The User Id.</param>
    /// <returns>The list of todos.</returns>
    public IList<ToDoItemViewModel> Get(int userId)
    {
      string uri = string.Format(serviceApiUrl + GetAllUrl, userId);
      var dataAsString = httpClient.GetStringAsync(uri).Result;
      return JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
    }

    /// <summary>
    /// Creates a task. UserId is taken from the model.
    /// </summary>
    /// <param name="item">The todo to create.</param>
    public void Post(ToDoItemViewModel task)
    {
      httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, task)
               .Result.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Updates a todo.
    /// </summary>
    /// <param name="item">The todo to update.</param>
    public void Put(ToDoItemViewModel task)
    {
      httpClient.PutAsJsonAsync(serviceApiUrl + UpdateUrl, task)
                .Result.EnsureSuccessStatusCode();
    }
  }
}