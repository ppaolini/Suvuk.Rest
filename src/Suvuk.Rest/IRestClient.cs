using Suvuk.Rest.Authentication;
using Suvuk.Rest.Serialization;

namespace Suvuk.Rest
{
    public interface IRestClient
    {
        IAuthenticationProvider AuthenticationProvider { get; set; }
        IContentSerializer RequestContentSerializer { get; set; }
        IContentSerializer ResponseContentSerializer { get; set; }
        bool EnsureSuccessStatusCode { get; set; }

        RestResponse<TOutput> Get<TOutput, TInput>(string relativeUri, TInput content);
        RestResponse<TOutput> Post<TOutput, TInput>(string relativeUri, TInput content);
    }
}
