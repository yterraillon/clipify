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

        private static string PlaylistEndpoint = "https://api.spotify.com/v1/users/{USERNAME}/playlists";

        public PlaylistClient(HttpClient client)
        {
            _client = client;
        }

        public Task<Playlist> GetPlaylistAsync(string token, string username, string playlistId,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var uri = new Uri(PlaylistEndpoint.Replace("{USERNAME}", username));

            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<Playlist>(uri, HttpMethod.Get, cancellationToken: cancellationToken);
        }
    }
}
