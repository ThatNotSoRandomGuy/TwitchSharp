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
    public class Channel {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("video_banner")]
        public string VideoBanner { get; set; }
        [JsonProperty("background")]
        public string Background { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string, object> Links { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
        [JsonProperty("_id")]
        public int Id { get; set; }
        [JsonProperty("mature")]
        public bool? Mature { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("broadcaster_language")]
        public string BroadcasterLanguage { get; set; }
        [JsonProperty("delay")]
        public int Delay { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("profile_banner")]
        public string ProfileBanner { get; set; }
        [JsonProperty("profile_banner_background_color")]
        public string ProfileBannerBackgroundColor { get; set; }
        [JsonProperty("partner")]
        public bool Partner { get; set; }
        [JsonProperty("views")]
        public int Views { get; set; }
        [JsonProperty("followers")]
        public int Followers { get; set; }
    }
}
