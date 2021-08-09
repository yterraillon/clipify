using System.Collections.Generic;
using System.Linq;
using Clipify.Application.Playlists.Models;

namespace Clipify.Application.Playlists.Requests.GetForkablePlaylists.Models
{
    public class ForkablePlaylistViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public IEnumerable<PlaylistImageViewModel> Images { get; set; } = Enumerable.Empty<PlaylistImageViewModel>();

        public static ForkablePlaylistViewModel Empty => new();
    }
}