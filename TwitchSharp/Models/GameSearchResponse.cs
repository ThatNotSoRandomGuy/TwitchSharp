using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    internal class GameSearchResponse {
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("games")]
        public IList<Game> Games { get; set; }
    }
}
