using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    /// <summary>
    /// https://developer.spotify.com/documentation/web-api/reference/#object-simplifiedplaylistobject
    /// </summary>
    public class SimplifiedPlaylistObject
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
        public PublicUserObject Owner { get; set; } = new ();

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = "playlist";

        [JsonProperty("uri")]
        public string PlaylistUri { get; set; } = string.Empty;
    }
}