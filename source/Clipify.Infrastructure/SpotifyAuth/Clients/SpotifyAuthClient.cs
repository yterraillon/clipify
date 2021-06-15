using Clipify.Application.Auth.Requests.Models;
using Clipify.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.SpotifyAuth.Clients
{
    public class SpotifyAuthClient
    {
        private readonly HttpClient _client;

        public SpotifyAuthClient(HttpClient client) => _client = client;

        public Task<TokenResponse> GetTokenAsync(Uri requestUri, IDictionary<string, string> parameters) =>
            _client.PostRequestAsync<TokenResponse>(requestUri, HttpMethod.Post, parameters);
    }
}