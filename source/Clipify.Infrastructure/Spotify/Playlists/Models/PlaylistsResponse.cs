using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clipify.Infrastructure.Spotify.Playlists.Models
{
    public class PlaylistsResponse
    {
        [JsonProperty("href")]
        public string Link { get; set; } = string.Empty;

        [JsonProperty("items")]
        public IEnumerable<PlaylistResponse> Items { get; set; } = new List<PlaylistResponse>();

        public static PlaylistsResponse Empty => new PlaylistsResponse();
    }
}