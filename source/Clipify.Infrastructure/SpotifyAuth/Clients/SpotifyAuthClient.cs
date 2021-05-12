using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using Clipify.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.SpotifyAuth.Clients
{
    public class SpotifyAuthClient
    {
        private HttpClient Client { get; }

        public SpotifyAuthClient(HttpClient client)
        {
            Client = client;
        }

        public Task<AccessTokenResponse> GetAccessTokenAsync(Uri requestUri, IDictionary<string, string> parameters)
        {
            return Client.PostRequestAsync<AccessTokenResponse>(requestUri, HttpMethod.Post, parameters);
        }
    }
}