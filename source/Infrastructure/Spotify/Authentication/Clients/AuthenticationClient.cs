using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Authentication.Requests.Models;
using Infrastructure.Extensions;

namespace Infrastructure.Spotify.Authentication.Clients
{
    public class AuthenticationClient
    {
        private readonly HttpClient _client;

        public AuthenticationClient(HttpClient client)
            => _client = client;

        public Task<TokenResponse> GetTokenAsync(Uri requestUri, IDictionary<string, string> parameters)
            => _client.PostRequestAsync<TokenResponse>(requestUri, HttpMethod.Post, parameters);
    }
}