using System.Collections.Generic;
using System.Linq;

namespace Clipify.Application.Playlists.Models
{
    /// <summary>
    /// TODO : should not be a "ViewModel" but a Playlist (maybe "SpotifyPlaylist")
    /// (Could it be in the domain as a ValueObject?)
    ///
    /// Queries should have their own ViewModels, ViewModels should not be shared too much.
    /// </summary>
    public class PlaylistViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string SnapshotId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public IEnumerable<PlaylistImageViewModel> Images { get; set; } = Enumerable.Empty<PlaylistImageViewModel>();

        public IEnumerable<TrackViewModel> Tracks { get; set; } = Enumerable.Empty<TrackViewModel>();

        public static PlaylistViewModel Empty => new ();
    }
}