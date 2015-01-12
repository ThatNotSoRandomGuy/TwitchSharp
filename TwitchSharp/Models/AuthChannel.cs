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
    public class AuthChannel {
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("stream_key")]
        public string StreamKey { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("teams")]
        public IList<Team> teams { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("video_banner")]
        public string VideoBanner { get; set; }
        [JsonProperty("background")]
        public string Background { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("mature")]
        public bool Mature { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
