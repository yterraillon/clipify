using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Spotify.Webapi.Models;
using Infrastructure.Spotify.Webapi.Playlists.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Webapi.Playlists.Clients
{
    public class PlaylistClient : ISpotifyPlaylistClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<PlaylistClient> _logger;
        private readonly Settings _settings;

        private const int SpotifyMaxPlaylistsByPage = 50;
        private const int SpotifyMaxOffset = 100000;

        public PlaylistClient(HttpClient client, IOptions<Settings> options, ILogger<PlaylistClient> logger)
        {
            _client = client;
            _logger = logger;
            _settings = options.Value;
        }

        public async Task<PagingObject<SimplifiedPlaylistObject>> GetPaginatedListOfCurrentUsersPlaylist(string accessToken, CancellationToken cancellationToken, int limit = 0, int offset = 0)
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
                _logger.LogError(ex.Message, ex);
                throw;
            }

            static bool IsLimitWithinBounds(int limit) => limit >= 0 && limit <= SpotifyMaxPlaylistsByPage;
            static bool IsOffsetWithinBounds(int offset) => offset >= 0 && offset <= SpotifyMaxOffset;
        }

        public async Task<PlaylistObject> GetPlaylist(string accessToken, string playlistId, CancellationToken cancellationToken)
        {
            try
            {
                var uri = _settings.BaseUrl + _settings.PlaylistEndpoint.Replace("{PLAYLIST_ID}", playlistId);
                var parameters = new Dictionary<string, string>
                {
                    {"q", "fields=tracks"}
                };

                _client.ConfigureAuthorizationHeader(accessToken);
                return await _client.GetWithQueryParametersAsync<PlaylistObject>(uri, parameters, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}