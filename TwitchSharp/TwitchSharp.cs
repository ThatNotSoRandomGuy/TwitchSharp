/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://www.wtfpl.net/ for more details. */
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitchSharp {
    public class TwitchSharp {

        #region Fields 

        private String _ClientID = null;
        private String _ClientSecret = null;
        private String _RedirectUrl = null;
        private Authenticator _AuthenticadeUser = null;

        private RestClient _RestClient;
        private Func<string, Parameter[], Method, IRestRequest> _requestFactory;

        private readonly String _TwitchRootAPI = "https://api.twitch.tv/kraken";
        private readonly String _AcceptHeader = "application/vnd.twitchtv.v2+json";

        #endregion

        #region Constructors

        public TwitchSharp(String clientId, String clientSecret, String redirectUrl) {
            _ClientID = clientId;
            _ClientSecret = clientSecret;
            _RedirectUrl = redirectUrl;

            _RestClient = new RestSharp.RestClient(_TwitchRootAPI);
            _requestFactory = (url, parameters, method) => {
                var req = new RestRequest(url, method);
                req.AddHeader("Accept", _AcceptHeader);
                req.AddHeader("Client-ID", _ClientID);
                foreach (Parameter pair in parameters ?? new Parameter[0]) {
                    req.AddParameter(pair.Name, pair.Value);
                }
                return req;
            };
        }

        public TwitchSharp(Authenticator authenticatedUser) {
            _ClientID = authenticatedUser.ClientID;
            _ClientSecret = authenticatedUser.ClientSecret;
            _RedirectUrl = authenticatedUser.RedirectUrl;
            _AuthenticadeUser = authenticatedUser;

            _RestClient = new RestSharp.RestClient(_TwitchRootAPI);
            _requestFactory = (url, parameters, method) => {
                var req = new RestRequest(url, method);
                req.AddHeader("Accept", _AcceptHeader);
                req.AddHeader("Client-ID", _ClientID);
                req.AddParameter("oauth_token", _AuthenticadeUser.GetAccessToken());
                foreach (Parameter pair in parameters ?? new Parameter[0]) {
                    req.AddParameter(pair.Name, pair.Value);
                }
                return req;
            };
        }

        #endregion

        #region Blocks

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Get user's block list </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <returns>
        ///     The list of blocked users.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        public IEnumerable<Models.User> GetBlockedUsers() {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.user_blocks_read)) throw new UserNotAuthenticatedException();

            var request = _requestFactory(
                "/users/" + _AuthenticadeUser.GetUsername() + "/blocks/", 
                null, 
                Method.GET);
            var result = JsonConvert.DeserializeObject<Models.BlockResult>(_RestClient.Execute(request).Content);

            return result.Blocks.Select(u => u.User);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Update user's block list </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <param name="target"> User that is going to be added to the block list </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public Boolean AddToBlockList(String target) {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.user_blocks_edit)) throw new UserNotAuthenticatedException();

            var request = _requestFactory(
                "/users/" + _AuthenticadeUser.GetUsername() + "/blocks/" + target,
                null,
                Method.PUT);
            var result = JsonConvert.DeserializeObject<Models.Block>(_RestClient.Execute(request).Content);

            return result != null;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Delete target from user's block list </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <param name="target"> User that is going to be removed from the block list. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public Boolean RemoveFromBlockList(String target) {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.user_blocks_edit)) throw new UserNotAuthenticatedException();

            var request = _requestFactory(
                "/users/" + _AuthenticadeUser.GetUsername() + "/blocks/" + target,
                null,
                Method.DELETE);
            var result = JsonConvert.DeserializeObject<Models.TwitchBase>(_RestClient.Execute(request).Content);

            return result.Status == 204;
        }

        #endregion

        #region Channel

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Returns a channel object. </summary>
        ///
        /// <param name="channel"> The channel. </param>
        ///
        /// <returns> The channel object. </returns>
        ///-------------------------------------------------------------------------------------------------
        public Models.Channel GetChannel(string channel) {
            var request = _requestFactory(
                "/channels/" + channel,
                null,
                Method.GET);
            return JsonConvert.DeserializeObject<Models.Channel>(_RestClient.Execute(request).Content);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Returns a channel object of authenticated user. </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <returns> The channel object. </returns>
        ///-------------------------------------------------------------------------------------------------
        public Models.AuthChannel GetChannel() {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.channel_read)) throw new UserNotAuthenticatedException();
            var request = _requestFactory(
                "/channel/",
                null,
                Method.GET);
            return JsonConvert.DeserializeObject<Models.AuthChannel>(_RestClient.Execute(request).Content);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Returns a list of user objects who are editors of a channel. </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <param name="channel"> The channel. </param>
        ///
        /// <returns>
        ///     A list containing the channel editors
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        public IEnumerable<Models.User> GetChannelEditors(String channel) {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.channel_read)) throw new UserNotAuthenticatedException();
            var request = _requestFactory(
                "/channels/" + channel + "/editors",
                null,
                Method.GET);
            var result = JsonConvert.DeserializeObject<Models.EditorsResult>(_RestClient.Execute(request).Content);

            return result.Users;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Update channel's status or game. </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <param name="channel"> The channel. </param>
        ///
        /// <returns>
        ///     The updated channel object
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        public Models.Channel UpdateChannelStatus(String channel, String _status, String _game) {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.channel_editor)) throw new UserNotAuthenticatedException();
            var request = _requestFactory(
                "/channels/" + channel,
                null,
                Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(
                "Application/Json", "{ \"channel\": {\"status\": " + _status + ",\"game\": "+ _game +"}}", ParameterType.RequestBody);

            return JsonConvert.DeserializeObject<Models.Channel>(_RestClient.Execute(request).Content);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Resets channel's stream key. </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <param name="channel"> The channel. </param>
        ///
        /// <returns> The reponse status code. 204 on success. </returns>
        ///-------------------------------------------------------------------------------------------------
        public Int32 ChannelResetStreamKey(String channel) {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.channel_stream)) throw new UserNotAuthenticatedException();

            var request = _requestFactory(
                "/channels/" + channel + "/stream_key/",
                null,
                Method.DELETE);
            var result = JsonConvert.DeserializeObject<Models.TwitchBase>(_RestClient.Execute(request).Content);

            return result.Status;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Start commercial on channel. </summary>
        ///
        /// <exception cref="UserNotAuthenticatedException">
        ///     Thrown if there's no authenticated user or the authentication scope is not sufficient.
        /// </exception>
        ///
        /// <param name="channel"> The channel. </param>
        /// <param name="length"> The commercial length. Valid values are 30, 60 and 90. </param>
        ///
        /// <returns> The reponse status code. 204 on success. 422 if length is not valid. </returns>
        ///-------------------------------------------------------------------------------------------------
        public Int32 ChannelStartCommercial(String channel, Int32 length) {
            if (_AuthenticadeUser == null || !_AuthenticadeUser.HasScope(Models.Scopes.channel_stream)) throw new UserNotAuthenticatedException();

            var request = _requestFactory(
                "/channels/" + channel + "/commercial/",
                new Parameter[]{
                    new Parameter(){Name="length", Value=length}
                },
                Method.POST);
            var result = JsonConvert.DeserializeObject<Models.TwitchBase>(_RestClient.Execute(request).Content);

            return result.Status;
        }

        #endregion

        #region Chat

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets a list of the emoticons available to subscribers of this channel. </summary>
        ///
        /// <param name="channel"> The channel. </param>
        ///
        /// <returns>
        ///     The emoticon list
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        public IEnumerable<Models.Emoticon> GetChannelEmoticons(String channel) {
            var request = _requestFactory(
                "/chat/" + channel + "/emoticons/",
                null,
                Method.GET);
            var result = JsonConvert.DeserializeObject<Models.EmoticonsResponse>(_RestClient.Execute(request).Content);

            return result.Emoticons.Select(e => e).Where(e => e.SubscriberOnly == true);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets a the badges for this channel. </summary>
        ///
        /// <param name="channel"> The channel. </param>
        ///
        /// <returns>
        ///     The badges objects
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        public Models.Badges GetChannelBadges(String channel) {
            var request = _requestFactory(
                "/chat/" + channel + "/badges/",
                null,
                Method.GET);
            var result = JsonConvert.DeserializeObject<Models.Badges>(_RestClient.Execute(request).Content);

            return result;
        }

        #endregion

        #region Follows

        //TODO...

        #endregion

        #region Games

        //TODO...

        #endregion

        #region Ingests

        //TODO...

        #endregion

        #region Root

        public Models.Root GetRootObject() {
            var request = new RestRequest("/", Method.GET);
            var responseString = _RestClient.Execute(request).Content;

            return JsonConvert.DeserializeObject<Models.Root>(responseString);
        }

        #endregion

        #region Search

        public IEnumerable<Models.Game> SearchGame(String query) {
            var request = _requestFactory(
                "/search/games",
                new Parameter[]{
                    new Parameter(){Name="query", Value=query},
                    new Parameter(){Name="live", Value=true},
                    new Parameter(){Name="type", Value="suggest"}
                },
                Method.GET);
            var result = JsonConvert.DeserializeObject<Models.GameSearchResponse>(_RestClient.Execute(request).Content);

            return result.Games;
        }

        public IEnumerable<Models.Game> SearchStreams(String query, int limit = 25, int offset = 0) {
            var request = _requestFactory(
                "/search/streams",
                new Parameter[]{
                    new Parameter(){Name="query", Value=query},
                    new Parameter(){Name="limit", Value=limit},
                    new Parameter(){Name="offset", Value=offset}
                },
                Method.GET);
            var result = JsonConvert.DeserializeObject<Models.GameSearchResponse>(_RestClient.Execute(request).Content);

            return result.Games;
        }

        #endregion

        #region Streams

        //TODO...

        #endregion

        #region Subscriptions

        //TODO...

        #endregion

        #region Teams

        //TODO...

        #endregion

        #region Users

        //TODO...

        #endregion

        #region Video

        //TODO...

        #endregion
    }
}
