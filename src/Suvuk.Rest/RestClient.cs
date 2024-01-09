using Suvuk.Rest.Authentication;
using Suvuk.Rest.Serialization;
using System;
using System.Net.Http;

namespace Suvuk.Rest
{
    public class RestClient : IRestClient
    {
        private HttpClient _httpClient;

        public IAuthenticationProvider AuthenticationProvider { get; set; }
        public IContentSerializer RequestContentSerializer { get; set; }
        public IContentSerializer ResponseContentSerializer { get; set; }
        public Uri BaseUrl { get; set; }
        public bool EnsureSuccessStatusCode { get; set; } = false;
        public TimeSpan Timeout { get => _httpClient.Timeout; set => _httpClient.Timeout = value; }

        public RestClient(string baseUrl = null) : this(new HttpClient(), baseUrl) { }

        public RestClient(HttpMessageHandler handler, bool disposeHandler = true, string baseUrl = null) : this(new HttpClient(handler, disposeHandler), baseUrl) { }

        public RestClient(HttpClient client, string baseUrl = null)
        {
            _httpClient = client;
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                BaseUrl = new Uri(baseUrl);
            }
        }

        public RestResponse<TOutput> Get<TOutput, TInput>(string relativeUri, TInput content)
        {
            return ExecuteRequest<TOutput, TInput>(relativeUri, HttpMethod.Get, content);
        }

        public RestResponse<TOutput> Post<TOutput, TInput>(string relativeUri, TInput content)
        {
            return ExecuteRequest<TOutput, TInput>(relativeUri, HttpMethod.Post, content);
        }

        public void Put() { }

        public void Delete() { }

        public void Patch(string relativeUri)
        {
            HttpMethod method;
#if !NET
            method = new HttpMethod("PATCH");
#else
            method = HttpMethod.Patch;
#endif
        }

        private RestResponse<TOutput> ExecuteRequest<TOutput, TInput>(string relativeUri, HttpMethod method, TInput content)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = method;
            request.RequestUri = new Uri(BaseUrl, relativeUri);
            RequestContentSerializer?.SerializeContent(request, content);

            AuthenticationProvider?.AddAuthentication(request);

            HttpResponseMessage response = _httpClient.SendAsync(request).Result;
            if (EnsureSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }

            return new RestResponse<TOutput>(response, ResponseContentSerializer);
        }
    }
}
