using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Models;
using Clipify.Infrastructure.Extensions;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistClient : IPlaylistClient
    {
        private readonly HttpClient _client;

        private const string PlaylistEndpoint = "https://api.spotify.com/v1/users/{USER_ID}/playlists";

        public PlaylistClient(HttpClient client)
        {
            _client = client;
        }

        public Task<PlaylistResponse> GetPlaylistAsync(string token, string userId, string playlistId,
            CancellationToken cancellationToken)
        {
            var uri = new Uri($"{PlaylistEndpoint.Replace("{USER_ID}", userId)}/{playlistId}");

            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);
        }

        public Task<PlaylistResponse> GetPlaylistsAsync(string token, string userId, CancellationToken cancellationToken = new CancellationToken())
        {
            var uri = new Uri(PlaylistEndpoint.Replace("{USER_ID}", userId));

            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);
        }
    }
}
