using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Models;
using Clipify.Infrastructure.Extensions;
using Clipify.Infrastructure.Spotify.Settings;
using Microsoft.Extensions.Options;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistClient : IPlaylistClient
    {
        private readonly HttpClient _client;

        private readonly SpotifyApiSettings _settings;

        public PlaylistClient(HttpClient client, IOptions<SpotifyApiSettings> settings)
        {
            _client = client;
            _settings = settings.Value;
        }

        public Task<PlaylistResponse> GetPlaylistAsync(string token, string userId, string playlistId,
            CancellationToken cancellationToken)
        {
            var uri = new Uri($"{_settings.BaseUrl}{_settings.PlaylistEndpoint.Replace("{USER_ID}", userId)}/{playlistId}");

            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);
        }

        public Task<PlaylistResponse> GetPlaylistsAsync(string token, string userId, CancellationToken cancellationToken = new CancellationToken())
        {
            var uri = new Uri(_settings.BaseUrl + _settings.PlaylistEndpoint.Replace("{USER_ID}", userId));

            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);
        }
    }
}
