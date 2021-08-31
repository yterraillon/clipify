using System;
using System.Diagnostics;
using System.Net.Http;
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

        public PlaylistClient(HttpClient client, IOptions<Settings> options)
        {
            _client = client;
            _settings = options.Value;
        }

        public async Task<PagingObject<SimplifiedPlaylistObject>> GetAListOfCurrentUsersPlaylist(string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                var uri = new Uri(_settings.BaseUrl + _settings.CurrentUserPlaylistsEndpoint);

                return await _client.ConfigureAuthorization(accessToken)
                    .PostRequestAsync<PagingObject<SimplifiedPlaylistObject>>(uri, HttpMethod.Get,
                        cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO : à supprimer ou remplacer par un logger
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}