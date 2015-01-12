/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://www.wtfpl.net/ for more details. */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    public class Team {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("background")]
        public object Background { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("logo")]
        public object Logo { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("_id")]
        public int Id { get; set; }
        [JsonProperty("info")]
        public string Info { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
}
