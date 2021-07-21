using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Clipify.Infrastructure.Spotify.Models.Playlist
{
    public class AddTracksToPlaylistResponse
    {
        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; } = string.Empty;
    }
}