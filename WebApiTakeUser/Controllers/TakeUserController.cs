using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiTakeUser.Response;

namespace WebApiTakeUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TakeUserController : ControllerBase
    {
        private const string _address = "https://api.github.com/users/takenet";

        private const string _userAgent = "TestApp";

        private readonly ILogger<TakeUserController> _logger;

        public TakeUserController(ILogger<TakeUserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/GetUser")]
        public async Task<string> Get()
        {
            var result = await GetAsync(_address);

            return result.ToString();
        }

        private async Task<JObject> GetAsync(string uri)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", _userAgent);
            var content = await client.GetStringAsync(uri);

            return JObject.Parse(content);
        }

        [HttpGet]
        [Route("/GetRepos")]
        public async Task<List<CardResponse>> GetRepos()
        {
            var result = await GetReposAsync(_address);

            return result;
        }

        private async Task<List<CardResponse>> GetReposAsync(string uri)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", _userAgent);
            var content = await client.GetStringAsync(uri+"/repos");

            List<CardResponse> lista = JsonConvert.DeserializeObject<List<CardResponse>>(content);

            return lista.Where(b => b.Linguagem != null && b.Linguagem.Equals("C#")).OrderByDescending(c => c.DataCriacao).Take(5).OrderBy(d => d.DataCriacao).ToList();
        }
    }
}
