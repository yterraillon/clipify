using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> PostRequestAsync<T>(this HttpClient client, Uri requestUri, HttpMethod method, IDictionary<string, string>? parameters = null, CancellationToken cancellationToken = new CancellationToken()) where T : new()
        {
            try
            {
                var response = await client
                    .SendAsync(new HttpRequestMessage(method, requestUri)
                    {
                        Content = new FormUrlEncodedContent(parameters)
                    }, cancellationToken);

                response.EnsureSuccessStatusCode();

                var content = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    NullValueHandling = NullValueHandling.Ignore
                }) ?? new T();
            }
            catch (HttpRequestException e)
            {
                // TODO: Error handling.
                Console.WriteLine(e);
                return new T();
            }
        }
    }
}