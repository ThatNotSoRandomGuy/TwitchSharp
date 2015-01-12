using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    internal class EditorsResult {
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}
