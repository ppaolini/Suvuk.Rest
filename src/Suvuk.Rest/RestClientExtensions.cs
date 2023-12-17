using System;

namespace Suvuk.Rest
{
    public static class RestClientExtensions
    {
        public static IRestClient WithEnsureSuccessStatusCode(this IRestClient client)
        {
            if (client != null)
            {
                client.EnsureSuccessStatusCode = true;
            }
            return client;
        }

        public static IRestClient WithTimeout(this IRestClient client, int milliseconds)
        {
            if (client != null)
            {
                client.Timeout = TimeSpan.FromMilliseconds(milliseconds);
            }
            return client;
        }
    }
}
