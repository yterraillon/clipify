using System.Collections.Generic;
using System.Linq;

namespace Clipify.Application.Playlists.Models
{
    public class TrackViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int TrackNumber { get; set; }

        public string Uri { get; set; } = string.Empty;

        public IEnumerable<ArtistViewModel> Artists { get; set; } = Enumerable.Empty<ArtistViewModel>();

        //public string Artists => string.Join(',', ArtistViewModels.Select(a => a.Name));

        public AlbumViewModel Album { get; set; } = AlbumViewModel.Empty;
    }

    public class ArtistViewModel
    {
        public string Name { get; set; } = string.Empty;
    }

    public class AlbumViewModel
    {
        public string Name { get; set; } = string.Empty;

        public static AlbumViewModel Empty => new();
    }
}