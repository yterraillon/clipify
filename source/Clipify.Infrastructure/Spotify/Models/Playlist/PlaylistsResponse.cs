using Newtonsoft.Json;
using System.Collections.Generic;

namespace Clipify.Infrastructure.Spotify.Models.Playlist
{
    public class PlaylistsResponse
    {
        [JsonProperty("href")]
        public string Link { get; set; } = string.Empty;

        [JsonProperty("items")]
        public IEnumerable<PlaylistResponse> Items { get; set; } = new List<PlaylistResponse>();

        [JsonIgnore]
        public static PlaylistsResponse Empty => new();
    }
}