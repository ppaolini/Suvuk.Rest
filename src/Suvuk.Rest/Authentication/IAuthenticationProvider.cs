using System.Net.Http;

namespace Suvuk.Rest.Authentication
{
    public interface IAuthenticationProvider
    {
        void AddAuthentication(HttpRequestMessage request);
    }
}
