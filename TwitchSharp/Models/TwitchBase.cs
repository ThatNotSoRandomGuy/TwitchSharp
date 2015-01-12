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
    public abstract class TwitchBase {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
