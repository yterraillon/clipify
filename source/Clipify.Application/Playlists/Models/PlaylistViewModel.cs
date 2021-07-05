using System.Collections.Generic;
using System.Linq;

namespace Clipify.Application.Playlists.Models
{
    public class PlaylistViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string SnapshotId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public IEnumerable<PlaylistImageViewModel> Images { get; set; } = Enumerable.Empty<PlaylistImageViewModel>();
        
        public IEnumerable<TrackViewModel> Tracks { get; set; } = Enumerable.Empty<TrackViewModel>();

        public static PlaylistViewModel Empty => new PlaylistViewModel();
    }
}