using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkyWeb.Repository.IRepositorys;

namespace ParkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IHttpClientFactory ClineFactory;


        public Repository(IHttpClientFactory clineFactory)
        {
            ClineFactory = clineFactory;
        }

        

        public async Task<T> GetAsync(string url, int Id, string token)
        {
            var requst = new HttpRequestMessage(HttpMethod.Get, url);
            var client = ClineFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(requst);
            if (response.StatusCode is System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }

            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var requst = new HttpRequestMessage(HttpMethod.Get, url);
            var client = ClineFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(requst);
            if (response.StatusCode is System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }

            return null;
        }

        public async Task<bool> CreateAsync(string url, T objToCreate, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), encoding: Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = ClineFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> UpdateAsync(string url, T objToUpdate, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (objToUpdate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToUpdate), encoding: Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = ClineFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public  async Task<bool> DeleteAsync(string url, int Id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url + Id);
            var client = ClineFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
