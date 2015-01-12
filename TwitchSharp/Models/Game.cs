using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    public class Game {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("popularity")]
        public int Popularity { get; set; }
        [JsonProperty("_id")]
        public int Id { get; set; }
        [JsonProperty("giantbomb_id")]
        public int Giantbomb_Id { get; set; }
        [JsonProperty("box")]
        public Images Box { get; set; }
        [JsonProperty("logo")]
        public Images Logo { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
    }
}
