using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    public class TrackObject
    {
        [JsonProperty("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty("episode")]
        public bool Episode { get; set; }

        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; } = string.Empty;

        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;

        [JsonProperty("album")]
        public AlbumObject Album { get; set; } = AlbumObject.Empty;

        [JsonProperty("artists")]
        public IEnumerable<ArtistObject> Artists { get; set; } = Enumerable.Empty<ArtistObject>();
    }
}