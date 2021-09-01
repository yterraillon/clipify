﻿using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Spotify.Webapi.Models;
using Infrastructure.Spotify.Webapi.Playlists.Models;

namespace Infrastructure.Spotify.Webapi.Playlists
{
    public interface ISpotifyPlaylistClient
    {
        Task<PagingObject<SimplifiedPlaylistObject>> GetAPaginatedListOfCurrentUsersPlaylist(string accessToken, CancellationToken cancellationToken, int limit = 0, int offset = 0);
    }
}