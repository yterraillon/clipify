using System;
using System.Net.Http;
using System.Threading.Tasks;
using Clipify.Application.Spotify.Clients;
using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using Clipify.Infrastructure.Extensions;

namespace Clipify.Infrastructure.Spotify.Clients
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly HttpClient _client;

        public SpotifyClient(HttpClient client)
        {
            _client = client;
        }

        public Task<ProfileResponse> GetUserProfileAsync(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return _client.PostRequestAsync<ProfileResponse>(new Uri("https://api.spotify.com/v1/me"), HttpMethod.Get, null);
        }
    }
}