namespace Suvuk.Rest.Serialization
{
    public static class RestClientSerializationExtensions
    {
        public static IRestClient WithSerializer<T>(this IRestClient restClient) where T : IContentSerializer, new()
        {
            if (restClient != null)
            {
                restClient.RequestContentSerializer = new T();
                restClient.ResponseContentSerializer = new T();
            }
            return restClient;
        }

        public static IRestClient WithRequestSerializer<T>(this IRestClient restClient) where T : IContentSerializer, new()
        {
            if (restClient != null)
            {
                restClient.RequestContentSerializer = new T();
            }
            return restClient;
        }

        public static IRestClient WithResponseSerializer<T>(this IRestClient restClient) where T : IContentSerializer, new()
        {
            if (restClient != null)
            {
                restClient.ResponseContentSerializer = new T();
            }
            return restClient;
        }
    }
}