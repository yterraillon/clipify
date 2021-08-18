using System;

namespace Domain.Entities
{
    public class User : Entity
    {
        public const string DefaultUserId = "default";

        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public Spotify.Profile SpotifyProfile { get; set; } = Spotify.Profile.Empty;

        public static User CreateFromSpotifyProfile(Spotify.Profile profile) =>
            new()
            {
                Username = profile.UserName,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                SpotifyProfile = profile
            };

        public static User Empty => new();

        public bool IsLoggedIntoSpotify() => SpotifyProfile.Id != string.Empty;
    }
}