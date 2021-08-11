using Clipify.Infrastructure.Spotify.Models.Track;
using Clipify.Infrastructure.Spotify.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clipify.Infrastructure.Spotify.Models.Playlist
{
    public class PlaylistTrackResponse
    {
        [JsonProperty("items")]
        public IEnumerable<PlaylistTrackItemResponse> Items { get; set; } = Enumerable.Empty<PlaylistTrackItemResponse>();

        [JsonIgnore]
        public static PlaylistTrackResponse Empty => new();
    }

    public class PlaylistTrackItemResponse
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("added_by"), JsonIgnore]
        public PublicUserResponse AddedBy { get; set; } = PublicUserResponse.Empty;

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("track")]
        public TrackResponse Track { get; set; } = TrackResponse.Empty;

        [JsonIgnore]
        public static PlaylistTrackItemResponse Empty => new();
    }
}