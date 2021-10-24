using System;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Playlists.Models
{
    public class PlaylistTrackObject
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("added_by")]
        public PublicUserObject AddedBy { get; set; } = new();

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("track")]
        public TrackObject Track { get; set; } = new();
    }
}