using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    public class AlbumObject
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

        public static AlbumObject Empty => new();
    }
}