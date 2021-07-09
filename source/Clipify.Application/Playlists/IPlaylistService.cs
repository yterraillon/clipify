using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using System.Collections.Generic;

namespace Clipify.Application.Playlists
{
    public interface IPlaylistService
    {
        public void CreatePlaylistWithTracks(Playlist playlist, IEnumerable<string> trackIds);

        public void AddTracksToPlaylist(string playlistId, IEnumerable<Track> tracks);

        public void AddTracksToPlaylist(Playlist playlist, IEnumerable<Track> tracks);
    }
}