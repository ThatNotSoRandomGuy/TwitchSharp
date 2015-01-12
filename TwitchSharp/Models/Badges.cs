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
    public class Badges {
        public class Badge {
            [JsonProperty("alpha")]
            public string Alpha { get; set; }
            [JsonProperty("image")]
            public string Image { get; set; }
            [JsonProperty("svg")]
            public string Svg { get; set; }
        }

        public class BadgeSubscriber {
            [JsonProperty("image")]
            public string Image { get; set; }
        }

        [JsonProperty("global_mod")]
        public Badge GlobalMod { get; set; }
        [JsonProperty("admin")]
        public Badge Admin { get; set; }
        [JsonProperty("broadcaster")]
        public Badge Broadcaster { get; set; }
        [JsonProperty("mod")]
        public Badge Mod { get; set; }
        [JsonProperty("staff")]
        public Badge Staff { get; set; }
        [JsonProperty("turbo")]
        public Badge Turbo { get; set; }
        [JsonProperty("subscriber")]
        public BadgeSubscriber Subscriber { get; set; }
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
    }
}
