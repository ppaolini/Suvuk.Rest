using System.Text;

namespace Suvuk.Rest.Authentication
{
    public static class RestClientAuthenticationExtensions
    {
        public static IRestClient WithOAuth2ClientCredentialsAuthentication(this IRestClient restClient, string tokenEndpoint, string clientId, string clientSecret)
        {
            if (restClient != null)
            {
                restClient.AuthenticationProvider = new OAuth2ClientCredentialsAuthenticationProvider(tokenEndpoint, clientId, clientSecret);
            }
            return restClient;
        }

        public static IRestClient WithBearerTokenAuthentication(this IRestClient restClient, string token)
        {
            if (restClient != null)
            {
                restClient.AuthenticationProvider = new BearerTokenAuthenticationProvider(token);
            }
            return restClient;
        }

        public static IRestClient WithBasicTokenAuthentication(this IRestClient restClient, string username, string password)
        {
            if (restClient != null)
            {
                restClient.AuthenticationProvider = new BasicAuthenticationProvider(username, password);
            }
            return restClient;
        }

        public static IRestClient WithBasicTokenAuthentication(this IRestClient restClient, string username, string password, Encoding encoding)
        {
            if (restClient != null)
            {
                restClient.AuthenticationProvider = new BasicAuthenticationProvider(username, password, encoding);
            }
            return restClient;
        }
    }
}
