using Microsoft.Net.Http.Headers;
using System.Net.Http;

namespace Suvuk.Rest.Authentication
{
    public class BearerTokenAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string _token;

        public BearerTokenAuthenticationProvider(string token) { _token = token; }

        public void AddAuthentication(HttpRequestMessage request)
        {
            request.Headers.Add(HeaderNames.Authorization, $"Bearer {_token}");
        }
    }
}
