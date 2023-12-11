using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Suvuk.Rest.Serialization
{
    public class JsonContentSerializer : IContentSerializer
    {
        public T DeserializeContent<T>(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<T>(response.Content.ReadAsStreamAsync().Result);
        }

        public void SerializeContent<T>(HttpRequestMessage request, T content)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }
    }
}
