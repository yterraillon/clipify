using System.Collections.Generic;
using System.Linq;

namespace Clipify.Infrastructure.Spotify.Models.Playlist
{
    public class AddTracksToPlaylistRequest
    {
        public int Position { get; set; }
        
        public IEnumerable<string> Uris { get; set; } = Enumerable.Empty<string>();
    }
}