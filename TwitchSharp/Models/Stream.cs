using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    public class Stream {
        [JsonProperty("_id")]
        public object Id { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("viewers")]
        public int Viewers { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created_at { get; set; }
        [JsonProperty("preview")]
        public Images Preview { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("channel")]
        public Channel channel { get; set; }
    }
}
