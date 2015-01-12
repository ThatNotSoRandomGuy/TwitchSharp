/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://www.wtfpl.net/ for more details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp.Models {
    public enum Scopes {
        user_read,
        user_blocks_edit,
        user_blocks_read,
        user_follows_edit,
        channel_read,
        channel_editor,
        channel_commercial,
        channel_stream,
        channel_subscriptions,
        user_subscriptions,
        channel_check_subscription,
        chat_login

    }
}
