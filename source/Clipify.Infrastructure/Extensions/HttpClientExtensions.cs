using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> PostRequestAsync<T>(this HttpClient client, Uri requestUri, HttpMethod method, IDictionary<string, string>? parameters = null, CancellationToken cancellationToken = new())
        {
            var response = await client
                .SendAsync(new HttpRequestMessage(method, requestUri)
                {
                    Content = parameters != null ? new FormUrlEncodedContent(parameters!) : null
                }, cancellationToken);

            response.EnsureSuccessStatusCode();

            var content = await response.Content
                .ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                NullValueHandling = NullValueHandling.Ignore
            }) ?? throw new JsonSerializationException("Failed to deserialize response content.");
        }
        
        public static async Task<TResponse> PostRequestAsJsonAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest request, CancellationToken cancellationToken = new())
        {
            var response = await client
                .PostAsJsonAsync(requestUri, request, cancellationToken);

            response.EnsureSuccessStatusCode();

            var content = await response.Content
                .ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResponse>(content, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                NullValueHandling = NullValueHandling.Ignore
            }) ?? throw new JsonSerializationException("Failed to deserialize response content.");
        }

        public static async Task<T> GetWithQueryParametersAsync<T>(this HttpClient client, string requestUri,
            IDictionary<string, string> parameters, CancellationToken cancellationToken = new())
        {
            var response = await client
                .GetAsync(QueryHelpers.AddQueryString(requestUri, parameters), cancellationToken);

            response.EnsureSuccessStatusCode();

            var content = await response.Content
                .ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                NullValueHandling = NullValueHandling.Ignore
            }) ?? throw new JsonSerializationException("Failed to deserialize response content.");
        }

        public static HttpClient ConfigureAuthorization(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return client;
        }
    }
}