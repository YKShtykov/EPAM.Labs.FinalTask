﻿using Client.Interfaces;
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
    public class ProxyRequestManager : IRequestManager
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ProxyUrl"];

        /// <summary>
        /// The url for getting all tasks.
        /// </summary>
        private const string GetAllUrl = "task?userId=";

        /// <summary>
        /// The url for updating a tasks.
        /// </summary>
        private const string UpdateUrl = "task/put/";


        /// <summary>
        /// The url for a tasks creation.
        /// </summary>
        private const string CreateUrl = "task/post/";


        /// <summary>
        /// The url for a tasks deletion.
        /// </summary>
        private const string DeleteUrl = "task/delete/";


        private readonly HttpClient httpClient;
        public ProxyRequestManager()
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
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlStr = serviceApiUrl + DeleteUrl + taskId;
                    httpClient.DeleteAsync(string.Format(urlStr))
                          .Result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw;
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
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlStr = serviceApiUrl + GetAllUrl + userId;
                    dataAsString = httpClient.GetStringAsync(urlStr).Result;
                    return JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
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
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlStr = serviceApiUrl + CreateUrl;
                    httpClient.PostAsJsonAsync(urlStr,task).Result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw;
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
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlStr = serviceApiUrl + UpdateUrl;
                    httpClient.PutAsJsonAsync(urlStr, task).Result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}