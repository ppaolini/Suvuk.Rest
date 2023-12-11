using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Suvuk.Rest.Serialization
{
    public class FormUrlEncodedContentSerializer : IContentSerializer
    {
        public T DeserializeContent<T>(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        public void SerializeContent<T>(HttpRequestMessage request, T content)
        {
            if (content is IEnumerable<KeyValuePair<string, string>> list)
            {
                request.Content = new FormUrlEncodedContent(list);
            }
            else
            {
                throw new ArgumentException("Content must be an IEnumerable<KeyValuePair<string, string>>.", nameof(content));
            }
        }
    }
}
