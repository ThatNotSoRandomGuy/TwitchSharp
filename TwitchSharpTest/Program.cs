using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchSharp;
using TwitchSharp.Models;

namespace TwitchSharpTest {
    class Program {
        
        static void Main(string[] args) {
            TwitchSharp.TwitchSharp _twitch;
            Authenticator authUser = new Authenticator( DefaultSettings.Default.clientID,
                                        DefaultSettings.Default.clientSecret,
                                        "http://localhost");

            authUser.BeginCodeAuthentication(
                new List<Scopes>() { 
                    Scopes.user_read,
                    Scopes.user_blocks_edit,
                    Scopes.user_blocks_read,
                    Scopes.user_follows_edit,
                    Scopes.channel_read,
                    Scopes.channel_editor,
                    Scopes.channel_commercial,
                    Scopes.channel_stream, 
                    Scopes.channel_subscriptions,
                    Scopes.user_subscriptions,
                    Scopes.channel_check_subscription,
                    Scopes.chat_login});
            
            string code = Console.ReadLine();

            if (!authUser.GetOAuthTokenFromCode(code)) {
                Console.WriteLine("Could not authenticate user!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("User successfully authenticated!");
            _twitch = new TwitchSharp.TwitchSharp(authUser);
            List<User> u = new List<User>(_twitch.GetBlockedUsers());

            u.ForEach(e => { Console.WriteLine(e.DisplayName); });

            Console.ReadKey();
            return;
        }
    }
}
