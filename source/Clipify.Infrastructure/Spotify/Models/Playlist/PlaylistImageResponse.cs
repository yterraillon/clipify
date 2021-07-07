using Newtonsoft.Json;

namespace Clipify.Infrastructure.Spotify.Models.Playlist
{
    public class PlaylistImageResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonIgnore]
        public static PlaylistImageResponse Empty => new();
    }
}