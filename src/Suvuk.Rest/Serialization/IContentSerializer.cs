using System.Net.Http;

namespace Suvuk.Rest.Serialization
{
    public interface IContentSerializer
    {
        void SerializeContent<T>(HttpRequestMessage request, T content);
        T DeserializeContent<T>(HttpResponseMessage response);
    }
}
