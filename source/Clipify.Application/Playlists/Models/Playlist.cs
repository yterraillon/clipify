using Newtonsoft.Json;

namespace Clipify.Application.Playlists.Models
{
    public class Playlist
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("uri")]
        public string PlaylistUri { get; set; } = string.Empty;

        [JsonProperty("public")]
        public bool PublicPlaylist { get; set; }

        [JsonProperty("collaborative")]
        public bool Collaborative { get; set; }
    }
}