using Clipify.Application;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using System.Collections.Generic;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist, string> _playlistRepository;

        private readonly IRepository<Track, string> _trackRepository;

        public PlaylistService(IRepository<Playlist, string> playlistRepository, IRepository<Track, string> trackRepository)
        {
            _playlistRepository = playlistRepository;
            _trackRepository = trackRepository;
        }

        public void CreatePlaylistWithTracks(Playlist playlist, IEnumerable<TrackViewModel> tracks)
        {
            foreach (var track in tracks)
            {
                var newTrack = Track.Create(playlist.Id, track.Id, track.Uri);

                playlist.TrackIds.Add(newTrack.Id);
                _trackRepository.Add(newTrack);
            }

            _playlistRepository.Add(playlist);
        }

        public void AddTracksToPlaylist(string playlistId, IEnumerable<Track> tracks)
        {
            var playlist = _playlistRepository.Get(playlistId);

            foreach (var track in tracks)
            {
                playlist.TrackIds.Add(track.Id);
            }

            _playlistRepository.Update(playlist);
        }

        public void AddTracksToPlaylist(Playlist playlist, IEnumerable<Track> tracks)
        {
            foreach (var track in tracks)
            {
                playlist.TrackIds.Add(track.Id);
            }

            _playlistRepository.Update(playlist);
        }
    }
}