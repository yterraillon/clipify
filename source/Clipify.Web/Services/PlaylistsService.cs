using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Extensions;
using Clipify.Web.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Clipify.Web.Services
{
    public class PlaylistsService
    {
        private readonly HttpClient _client;

        private readonly ApiSettings _apiSettings;

        public PlaylistsService(HttpClient client, IOptions<ApiSettings> apiSettings)
        {
            _client = client;
            _apiSettings = apiSettings.Value;
        }

        public Task<PlaylistViewModel> GetPlaylist(string id)
        {
            return _client.PostRequestAsync<PlaylistViewModel>(new Uri($"{_apiSettings.BaseUrl}/Playlists/{id}"), HttpMethod.Get);
        }

        public Task<IEnumerable<Playlist>> GetLocalPlaylists()
        {
            return _client.PostRequestAsync<IEnumerable<Playlist>>(new Uri($"{_apiSettings.BaseUrl}/LocalPlaylists"), HttpMethod.Get);
        }

        public Task<IEnumerable<PlaylistViewModel>> GetPlaylists()
        {
            return _client.PostRequestAsync<IEnumerable<PlaylistViewModel>>(new Uri($"{_apiSettings.BaseUrl}/Playlists"), HttpMethod.Get);
        }

        public Task DeleteLocalPlaylist(string playlistId)
        {
            return _client.DeleteAsync(new Uri($"{_apiSettings.BaseUrl}/Playlists/{playlistId}"));
        }

        public Task SavePlaylist(string playlistId, string snapshotId, string name)
        {
            return _client.PostAsJsonAsync(new Uri($"{_apiSettings.BaseUrl}/Playlists"),
                new {playlistId, snapshotId, name});
        }
    }
}