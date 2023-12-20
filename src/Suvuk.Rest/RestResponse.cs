using Suvuk.Rest.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Suvuk.Rest
{
    public class RestResponse<T>
    {
        private HttpResponseMessage _response;
        private IContentSerializer _serializer;

        public T Content
        {
            get
            {
                return _serializer.DeserializeContent<T>(_response);
            }
        }

        public string ContentAsString
        {
            get
            {
                return _response.Content.ReadAsStringAsync().Result;
            }
        }

        public bool IsSuccessStatusCode => _response.IsSuccessStatusCode;
        public string ReasonPhrase => _response.ReasonPhrase;
        public Version Version => _response.Version;
        public HttpStatusCode StatusCode => _response.StatusCode;
        public HttpResponseHeaders Headers => _response.Headers;
        public HttpContent RawContent => _response.Content;
#if NET
        public HttpResponseHeaders TrailingHeaders => _response.TrailingHeaders;
#endif

        public RestResponse(HttpResponseMessage response, IContentSerializer serializer)
        {
            _response = response;
            _serializer = serializer;
        }
    }
}
