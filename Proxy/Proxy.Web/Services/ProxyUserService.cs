using Proxy.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Proxy.Web.Services
{
    public class ProxyUserService : IUserService
    {
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        private const string CreateUrl = "Users";

        public int CreateUser(string userName)
        {            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, userName).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsAsync<int>().Result;
                }
            }
            catch (Exception ex)
            {

            }
            return -1;
        }

        public int GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public bool Login(int userId)
        {
            throw new NotImplementedException();
        }

        public void Logout(int userId)
        {
            throw new NotImplementedException();
        }
    }
}