using Proxy.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proxy.Web.Models;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Proxy.Web.Infrustructure
{
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ProxyManager : IRequestManager
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The url for getting all todos.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";

        /// <summary>
        /// The url for updating a todo.
        /// </summary>
        private const string UpdateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's creation.
        /// </summary>
        private const string CreateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's deletion.
        /// </summary>
        private const string DeleteUrl = "ToDos/{0}";

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
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
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<ToDoItemViewModel> Get(int userId)
        {
            string dataAsString = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    dataAsString = httpClient.GetStringAsync(string.Format(serviceApiUrl + GetAllUrl, userId)).Result;
                    return JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
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
    }
}