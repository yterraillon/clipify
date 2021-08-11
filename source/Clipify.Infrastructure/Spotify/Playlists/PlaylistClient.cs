using AutoMapper;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Extensions;
using Clipify.Infrastructure.Spotify.Models.Playlist;
using Clipify.Infrastructure.Spotify.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistClient : IPlaylistClient
    {
        private readonly HttpClient _client;

        private readonly IMapper _mapper;

        private readonly SpotifyApiSettings _settings;

        public PlaylistClient(HttpClient client, IOptions<SpotifyApiSettings> settings, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<PlaylistViewModel> GetPlaylistAsync(string token, string userId, string playlistId,
            CancellationToken cancellationToken)
        {
            var uri = new Uri($"{_settings.BaseUrl}{_settings.UserPlaylistsEndpoint.Replace("{USER_ID}", userId)}/{playlistId}");

            var response = await _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);

            return _mapper.Map<PlaylistViewModel>(response);
        }

        public async Task<IEnumerable<PlaylistViewModel>> GetPlaylistsAsync(string token, string userId, CancellationToken cancellationToken)
        {
            var uri = new Uri(_settings.BaseUrl + _settings.UserPlaylistsEndpoint.Replace("{USER_ID}", userId));

            var response = await _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistsResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<PlaylistViewModel>>(response.Items);
        }

        public async Task<PlaylistViewModel> GetPlaylistWithTracksAsync(string token, string playlistId,
            CancellationToken cancellationToken)
        {
            var uri = _settings.BaseUrl + _settings.PlaylistEndpoint.Replace("{PLAYLIST_ID}", playlistId);
            var parameters = new Dictionary<string, string>
            {
                {"q", "fields=tracks"}
            };

            var response = await _client
                .ConfigureAuthorization(token)
                .GetWithQueryParametersAsync<PlaylistWithTracksResponse>(uri, parameters, cancellationToken);

            return _mapper.Map<PlaylistViewModel>(response);
        }

        public async Task<PlaylistViewModel> CreatePlaylistAsync(string token, string userId, string name, CancellationToken cancellationToken)
        {
            var uri = _settings.BaseUrl + _settings.UserPlaylistsEndpoint.Replace("{USER_ID}", userId);
            var response = await _client.ConfigureAuthorization(token)
                .PostRequestAsJsonAsync<CreatePlaylistRequest, PlaylistResponse>(uri, new CreatePlaylistRequest
                {
                    Name = name
                }, cancellationToken);

            return _mapper.Map<PlaylistViewModel>(response);
        }

        public async Task<string> AddTracksToPlaylistAsync(string token, string playlistId, IEnumerable<Track> tracks, CancellationToken cancellationToken)
        {
            var uri = _settings.BaseUrl + _settings.PlaylistTracksEndpoint.Replace("{PLAYLIST_ID}", playlistId);
            var response = await _client.ConfigureAuthorization(token)
                .PostRequestAsJsonAsync<AddTracksToPlaylistRequest, AddTracksToPlaylistResponse>(uri,
                    new AddTracksToPlaylistRequest
                    {
                        Uris = tracks.Select(x => x.Uri)
                    }, cancellationToken);

            return response.SnapshotId;
        }
    } 
}
