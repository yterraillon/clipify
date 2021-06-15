using AutoMapper;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Models;
using Clipify.Infrastructure.Extensions;
using Clipify.Infrastructure.Spotify.Playlists.Models;
using Clipify.Infrastructure.Spotify.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
            var uri = new Uri($"{_settings.BaseUrl}{_settings.PlaylistEndpoint.Replace("{USER_ID}", userId)}/{playlistId}");

            var response = await _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);

            return _mapper.Map<PlaylistViewModel>(response ?? PlaylistResponse.Empty);
        }

        public async Task<IEnumerable<PlaylistViewModel>> GetPlaylistsAsync(string token, string userId, CancellationToken cancellationToken = new CancellationToken())
        {
            var uri = new Uri(_settings.BaseUrl + _settings.PlaylistEndpoint.Replace("{USER_ID}", userId));

            var response = await _client.ConfigureAuthorization(token)
                .PostRequestAsync<PlaylistsResponse>(uri, HttpMethod.Get, cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<PlaylistViewModel>>(response.Items);
        }
    }
}
