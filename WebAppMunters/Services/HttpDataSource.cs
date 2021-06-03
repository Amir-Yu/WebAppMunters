using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using WebAppMunters.Models;

namespace WebAppMunters.Services
{
    public class HttpDataSource
    {
        private string apiKey;
        private readonly IConfiguration _configuration;
        private static IHttpClientFactory _clientFactory;
        
        public HttpDataSource(IConfiguration iConfig, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _configuration = iConfig;
            apiKey = _configuration.GetValue<string>("ApiKey");
        }

        private static async Task<T> GiptyHttpReqTask<T>(string url)
        {
            var client = _clientFactory.CreateClient("gipty");
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                //TODO: handle bad requests
                throw new Exception();
            }
        }

        public async Task<GiptyResponce> GiptySearch(string searchVal, int resLimit)
        {
            var url = $"search?api_key={apiKey}&q={searchVal}&limit={resLimit}&offset=0&rating=g&lang=en";
            return await GiptyHttpReqTask<GiptyResponce>(url) as GiptyResponce;
        }

        public async Task<GiptyResponce> GiptyTrending(int resLimit)
        {
            var url = $"trending?api_key={apiKey}&limit={resLimit}&rating=g";
            return await GiptyHttpReqTask<GiptyResponce>(url) as GiptyResponce;
        }
    }
}
