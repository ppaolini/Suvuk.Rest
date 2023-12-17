using Suvuk.Rest.Authentication;
using Suvuk.Rest.Serialization;
using System;

namespace Suvuk.Rest
{
    public interface IRestClient
    {
        IAuthenticationProvider AuthenticationProvider { get; set; }
        IContentSerializer RequestContentSerializer { get; set; }
        IContentSerializer ResponseContentSerializer { get; set; }
        bool EnsureSuccessStatusCode { get; set; }
        TimeSpan Timeout { get; set; }

        RestResponse<TOutput> Get<TOutput, TInput>(string relativeUri, TInput content);
        RestResponse<TOutput> Post<TOutput, TInput>(string relativeUri, TInput content);
    }
}
