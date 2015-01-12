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
    public class Emoticon {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("regex")]
        public string Regex { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("subscriber_only")]
        public bool SubscriberOnly { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
