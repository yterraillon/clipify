using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clipify.Web.Services
{
    public class PlaylistService
    {
        private readonly HttpClient _client;

        public PlaylistService(HttpClient client)
        {
            _client = client;
        }

        public Task<PlaylistViewModel> GetPlaylist(string id)
        {
            // TODO: uri.
            return _client.PostRequestAsync<PlaylistViewModel>(new Uri($".../{id}"), HttpMethod.Get);
        }

        public Task<IEnumerable<Playlist>> GetPlaylists()
        {
            // TODO: uri.
            return _client.PostRequestAsync<IEnumerable<Playlist>>(new Uri("..."), HttpMethod.Get);
        }
    }
}