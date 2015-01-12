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
    public class Authorization {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("scopes")]
        public IEnumerable<string> Scopes { get; set; }
    }
}
