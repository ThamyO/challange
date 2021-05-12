using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTakeUser.Response
{
    public class CardResponse
    {
        [JsonProperty("owner")]
        public owner owner { get; set; }
        [JsonProperty("html_url")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string SubTitle { get; set; }
        [JsonProperty("created_at")]
        public DateTime DataCriacao { get; set; }
        [JsonProperty("language")]
        public string Linguagem { get; set; }
    }

    public class owner
    {
        [JsonProperty("avatar_url")]
        public string avatar { get; set; }
    }
}
