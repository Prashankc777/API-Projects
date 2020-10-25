using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WEB.IRepositorys;
using System.Net;

namespace WEB.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public  async  Task<T> GetAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+id);

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url );

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate is null) return false;
            request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), encoding:Encoding.UTF8,"application/json");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async  Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (objToUpdate is null) return false;
            request.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate), encoding: Encoding.UTF8,
                "application/json");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+id);

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode is HttpStatusCode.NoContent)
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
