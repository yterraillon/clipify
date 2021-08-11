using Newtonsoft.Json;

namespace Clipify.Infrastructure.Spotify.Models.Track
{
    public class Album
    {
        [JsonProperty("album_type")]
        public string AlbumType { get; set; } = string.Empty;

        [JsonProperty("href")]
        public string Href { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = "album";

        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;

        public static Album Empty => new();
    }
}