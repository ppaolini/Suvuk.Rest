using Microsoft.Net.Http.Headers;
using Suvuk.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Suvuk.Rest.Authentication
{
    public class OAuth2ClientCredentialsAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        private readonly IRestClient _restClient;
        private string _accessToken;
        private DateTime _tokenExpiration;
        private int _tokenExpirationMargin;

        public OAuth2ClientCredentialsAuthenticationProvider(string tokenEndpoint, string clientId, string clientSecret, int tokenExpirationMargin = 15) : this(
            new RestClient(tokenEndpoint)
                .WithRequestSerializer<FormUrlEncodedContentSerializer>()
                .WithResponseSerializer<JsonContentSerializer>()
                .WithEnsureSuccessStatusCode(),
            clientId,
            clientSecret,
            tokenExpirationMargin)
        { }

        public OAuth2ClientCredentialsAuthenticationProvider(IRestClient restClient, string clientId, string clientSecret, int tokenExpirationMargin = 15)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _restClient = restClient;
            _tokenExpiration = DateTime.Now.AddSeconds(-1);
            _tokenExpirationMargin = tokenExpirationMargin;
        }

        public void AddAuthentication(HttpRequestMessage request)
        {
            if (DateTime.Now > _tokenExpiration)
            {
                RefreshToken();
            }
            request.Headers.Add(HeaderNames.Authorization, $"Bearer {_accessToken}");
        }

        private void RefreshToken()
        {
            Dictionary<string, string> content = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _clientId },
                { "client_secret", _clientSecret }
            };
            RestResponse<OAuth2TokenResponse> response = _restClient.Post<OAuth2TokenResponse, Dictionary<string, string>>("", content);

            OAuth2TokenResponse tokenResponse = response.Content;
            _accessToken = tokenResponse.AccessToken;
            // set new token expiration time with a safety margin to account for processing time and network latency
            _tokenExpiration = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn - Math.Min(tokenResponse.ExpiresIn / 2, _tokenExpirationMargin));
        }
    }
}
