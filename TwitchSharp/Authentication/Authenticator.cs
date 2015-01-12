/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://www.wtfpl.net/ for more details. */
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchSharp {

    public class Authenticator {
        public String ClientID;
        public String ClientSecret;
        public String RedirectUrl;

        private Models.Root _RootObject;
        private Dictionary<String, Object> _oauthResponse;

        private RestClient _RestClient;
        private Func<string, Parameter[], Method, IRestRequest> _requestFactory;

        private readonly String _TwitchRootAPI = "https://api.twitch.tv/kraken";
        private readonly String _AcceptHeader = "application/vnd.twitchtv.v2+json";
        
        public Authenticator(String clientId, String clientSecret, String redirectUrl) {
            ClientID = clientId;
            ClientSecret = clientSecret;
            RedirectUrl = redirectUrl;
            _RestClient = new RestSharp.RestClient(_TwitchRootAPI);
            _requestFactory = (url, parameters, method) => {
                var req = new RestRequest(url, method);
                req.AddHeader("Accept", _AcceptHeader);
                req.AddHeader("Client-ID", ClientID);
                foreach (Parameter pair in parameters) {
                    req.AddParameter(pair.Name, pair.Value);
                }
                return req;
            };
        }

        public void BeginCodeAuthentication(IEnumerable<Models.Scopes> scopes) {
            string url = "https://api.twitch.tv/kraken/oauth2/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=" + RedirectUrl + "&scope=";

            string _scopes = "";
            foreach (Models.Scopes scope in scopes) {
                _scopes = _scopes + Enum.GetName(typeof(Models.Scopes), scope) + "+";
            }

            _scopes = _scopes.Remove(_scopes.Length - 1, 1);

            System.Diagnostics.Process.Start(url + _scopes);
        }

        public bool GetOAuthTokenFromCode(String code) {
            try {
                var request = _requestFactory(
                    "/oauth2/token",
                    new Parameter[] { 
                    new Parameter(){Name="client_id",Value=ClientID},
                    new Parameter(){Name="client_secret",Value=ClientSecret},
                    new Parameter(){Name="grant_type",Value="authorization_code"},
                    new Parameter(){Name="redirect_uri",Value=RedirectUrl},
                    new Parameter(){Name="code",Value=code}
                    },
                    Method.POST);
                _oauthResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(_RestClient.Execute(request).Content);

                request = _requestFactory(
                    "/",
                    new Parameter[] { 
                        new Parameter(){Name="oauth_token",Value=_oauthResponse["access_token"]}
                    },
                    Method.GET);

                _RootObject = JsonConvert.DeserializeObject<Models.Root>(_RestClient.Execute(request).Content);
                return true;
            } catch (KeyNotFoundException) {
                return false;
            }
        }

        public String GetUsername() {
            if (_RootObject != null && _RootObject.Token != null)
                return _RootObject.Token.UserName;
            else return null;
        }

        public String GetAccessToken() {
            if (_oauthResponse != null)
                return (String)_oauthResponse["access_token"];
            else return null;
        }

        public String GetRefreshToken() {
            if (_oauthResponse != null)
                return (String)_oauthResponse["refresh_token"];
            else return null;
        }

        public bool HasScope(Models.Scopes scope) {
            return _RootObject.Token.Authorization.Scopes.Contains(Enum.GetName(typeof(Models.Scopes), scope));
        }
    }
}
