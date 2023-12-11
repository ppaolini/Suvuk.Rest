using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Text;

namespace Suvuk.Rest.Authentication
{
    public class BasicAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string _username;
        private readonly string _password;
        private readonly Encoding _encoding;

        public BasicAuthenticationProvider(string username, string password) : this(username, password, Encoding.UTF8) { }
        public BasicAuthenticationProvider(string username, string password, Encoding encoding)
        {
            _username = username;
            _password = password;
            _encoding = encoding;
        }

        public void AddAuthentication(HttpRequestMessage request)
        {
            string base64Credentials = Convert.ToBase64String(_encoding.GetBytes($"{_username}:{_password}"));
            request.Headers.Add(HeaderNames.Authorization, $"Basic {base64Credentials}");
        }
    }
}
