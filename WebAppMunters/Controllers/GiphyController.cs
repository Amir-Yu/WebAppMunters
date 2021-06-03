using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using WebAppMunters.Models;
using WebAppMunters.Services;

namespace WebAppMunters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiphyController : ControllerBase
    {
        private readonly HttpDataSource _httpDataSource;
        private IMemoryCache _memoryCache;

        public GiphyController(IConfiguration iConfig, IHttpClientFactory clientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _httpDataSource = new HttpDataSource(iConfig, clientFactory);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchVal, [FromQuery] int resLimit = 25)
        {
            var hashKey = $"{searchVal}{(resLimit <= 25 ? 25 : resLimit).ToString()}".GetHashCode();

            GiptyResponce searchResults = await _memoryCache.GetOrCreateAsync<GiptyResponce>(hashKey, 
                async entry => await _httpDataSource.GiptySearch(searchVal, resLimit));
            var results = searchResults.data.Select(
                d => new ResponceDataSet(d.id, d.title, d.images.original.url)).Take(resLimit);
            return Ok(results);
        }

        [HttpGet("trending")]
        public async Task<IActionResult> Trending([FromQuery] int resLimit = 25)
        {
            var hashKey = (resLimit <= 25 ? 25 : resLimit).GetHashCode();

            GiptyResponce searchResults = await _memoryCache.GetOrCreateAsync<GiptyResponce>(hashKey,
                async entry => await _httpDataSource.GiptyTrending(resLimit));
            var results = searchResults.data.Select(
                d => new ResponceDataSet(d.id, d.title, d.images.original.url)).Take(resLimit);
            return Ok(results);
        }
    }
}
