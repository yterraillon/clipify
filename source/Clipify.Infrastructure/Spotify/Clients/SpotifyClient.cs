using Clipify.Application.Spotify.Clients;
using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using Clipify.Infrastructure.Extensions;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.Spotify.Clients
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly HttpClient _client;

        public SpotifyClient(HttpClient client)
        {
            _client = client;
        }

        public Task<ProfileResponse> GetUserProfileAsync(string token, CancellationToken cancellationToken)
        {
            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<ProfileResponse>(
                    new Uri("https://api.spotify.com/v1/me"),
                    HttpMethod.Get,
                    cancellationToken: cancellationToken);
        }
    }
}