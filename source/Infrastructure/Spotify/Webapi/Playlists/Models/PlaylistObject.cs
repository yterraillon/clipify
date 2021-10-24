using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    public class PlaylistObject
    {
        [JsonProperty("collaborative")]
        public bool Collaborative { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("href")]
        public string Href { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("images")]
        public IEnumerable<ImageObject> Images { get; set; } = Enumerable.Empty<ImageObject>();

        [JsonProperty("name")]
        public string? Name { get; set; } = string.Empty;

        [JsonProperty("owner")]
        public PublicUserObject Owner { get; set; } = new();

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; } = string.Empty;

        [JsonProperty("tracks")]
        public IEnumerable<PlaylistTrackObject> Tracks { get; set; } = Enumerable.Empty<PlaylistTrackObject>();

        [JsonProperty("type")]
        public string Type { get; set; } = "playlist";

        [JsonProperty("uri")]
        public string PlaylistUri { get; set; } = string.Empty;
    }
}