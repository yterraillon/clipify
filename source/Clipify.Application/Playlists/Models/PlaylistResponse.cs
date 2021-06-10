using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clipify.Application.Playlists.Models
{
    public class Item
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

    public class PlaylistResponse
    {
        [JsonProperty("href")]
        public string Link { get; set; } = string.Empty;

        [JsonProperty("items")]
        public IEnumerable<Item> Items { get; set; } = new List<Item>();

        public static PlaylistResponse Empty => new PlaylistResponse();
    }
}