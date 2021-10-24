using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Playlists.Queries.GetPlaylist
{
    public record PlaylistViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public string CoverImage { get; set; } = "about:blank";
        public IEnumerable<TrackViewModel> Tracks { get; set; } = Enumerable.Empty<TrackViewModel>();
    }

    public record TrackViewModel (string Title, IEnumerable<string> Artists, string Album, DateTime? Added);
}