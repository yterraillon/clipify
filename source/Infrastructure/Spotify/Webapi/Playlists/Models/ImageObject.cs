using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    /// <summary>
    /// https://developer.spotify.com/documentation/web-api/reference/#object-imageobject
    /// </summary>
    public class ImageObject
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")] public string Url { get; set; } = string.Empty;

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}