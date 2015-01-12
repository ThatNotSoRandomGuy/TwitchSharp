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
    internal class EmoticonsResponse {
        [JsonProperty("_links")]
        public Dictionary<string,object> _links { get; set; }
        [JsonProperty("emoticons")]
        public IList<Emoticon> Emoticons { get; set; }
    }
}
