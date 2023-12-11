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
    }
}
