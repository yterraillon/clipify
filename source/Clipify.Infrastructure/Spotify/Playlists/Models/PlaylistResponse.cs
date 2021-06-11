using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clipify.Infrastructure.Spotify.Playlists.Models
{
    public class PlaylistResponse
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

        [JsonProperty("images")]
        public IEnumerable<PlaylistImageResponse> Images { get; set; } = new List<PlaylistImageResponse>();

        public static PlaylistResponse Empty => new PlaylistResponse();
    }
}