using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    /// <summary>
    /// https://developer.spotify.com/documentation/web-api/reference/#object-publicuserobject
    /// </summary>
    public class PublicUserObject
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonProperty("href")]
        public string Href { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("images")]
        public IEnumerable<ImageObject> Images { get; set; } = Enumerable.Empty<ImageObject>();

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}