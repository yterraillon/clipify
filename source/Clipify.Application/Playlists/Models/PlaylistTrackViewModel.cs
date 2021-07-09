using System.Collections.Generic;
using System.Linq;

namespace Clipify.Application.Playlists.Models
{
    public class PlaylistTrackViewModel
    {
        public IEnumerable<TrackViewModel> Tracks { get; set; } = Enumerable.Empty<TrackViewModel>();
    }
}