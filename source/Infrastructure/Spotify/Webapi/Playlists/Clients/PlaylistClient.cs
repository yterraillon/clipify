using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Spotify.Webapi.Models;
using Infrastructure.Spotify.Webapi.Playlists.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Webapi.Playlists.Clients
{
    public class PlaylistClient : ISpotifyPlaylistClient
    {
        private readonly HttpClient _client;
        private readonly Settings _settings;

        private const int SpotifyMaxPlaylistsByPage = 50;
        private const int SpotifyMaxOffset = 100000;

        public PlaylistClient(HttpClient client, IOptions<Settings> options)
        {
            _client = client;
            _settings = options.Value;
        }

        public async Task<PagingObject<SimplifiedPlaylistObject>> GetAPaginatedListOfCurrentUsersPlaylist(string accessToken, CancellationToken cancellationToken, int limit = 0, int offset = 0)
        {
            if (!IsLimitWithinBounds(limit) || !IsOffsetWithinBounds(offset))
                throw new ArgumentException("Invalid arguments for Spotify Webapi");

            try
            {
                var uri = new Uri($"{_settings.BaseUrl}{_settings.CurrentUserPlaylistsEndpoint}?limit={limit}&offset={offset}");
                _client.ConfigureAuthorizationHeader(accessToken);

                return await _client.PostRequestAsync<PagingObject<SimplifiedPlaylistObject>>(uri, HttpMethod.Get,
                        cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO : à supprimer ou remplacer par un logger
                Debug.WriteLine(ex);
                throw;
            }
        }

        private static bool IsLimitWithinBounds(int limit) => limit >= 0 && limit <= SpotifyMaxPlaylistsByPage;
        private static bool IsOffsetWithinBounds(int offset) => offset >= 0 && offset <= SpotifyMaxOffset;
    }
}