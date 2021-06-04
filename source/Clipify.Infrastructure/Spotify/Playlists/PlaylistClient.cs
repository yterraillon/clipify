using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Clipify.Application.Playlists;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistClient : IPlaylistClient
    {
        private readonly HttpClient _client;

        public PlaylistClient(HttpClient client)
        {
            _client = client;
        }
    }
}
