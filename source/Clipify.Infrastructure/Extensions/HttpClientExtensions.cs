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
        public static async Task<T> PostRequestAsync<T>(this HttpClient client, Uri requestUri, HttpMethod method, IDictionary<string, string>? parameters = null, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var response = await client
                    .SendAsync(new HttpRequestMessage(method, requestUri)
                    {
                        Content = parameters != null ? new FormUrlEncodedContent(parameters) : null
                    }, cancellationToken);

                response.EnsureSuccessStatusCode();

                var content = await response.Content
                    .ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            catch (HttpRequestException e)
            {
                // TODO: Error handling.
                Console.WriteLine(e);
                throw;
            }
        }

        public static HttpClient ConfigureAuthorization(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return client;
        }
    }
}