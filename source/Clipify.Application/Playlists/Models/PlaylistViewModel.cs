using System.Collections.Generic;

namespace Clipify.Application.Playlists.Models
{
    public class PlaylistViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public IEnumerable<PlaylistImageViewModel> Images { get; set; } = new List<PlaylistImageViewModel>();

        public static PlaylistViewModel Empty => new PlaylistViewModel();
    }
}