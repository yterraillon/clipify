using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using System.Collections.Generic;

namespace Clipify.Application.Playlists
{
    public interface IPlaylistService
    {
        public void AddPlaylistWithTracks(Playlist playlist, IEnumerable<TrackViewModel> tracks);
        
        public void AddTracks(string playlistId, IEnumerable<Track> tracks);

        public void AddTracks(Playlist playlist, IEnumerable<Track> tracks);
    }
}