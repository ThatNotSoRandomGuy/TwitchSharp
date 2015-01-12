using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    class SearchStreamResponse {
        [JsonProperty("_total")]
        public int _total { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("streams")]
        public IList<Stream> Streams { get; set; }
    }

}
