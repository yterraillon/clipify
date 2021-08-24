using System;

namespace Domain.Entities
{
    public class UserProfile : UniqueEntity
    {
        public const string DefaultUserId = "default";
        public UserProfile() : base(DefaultUserId)
        {
        }

        public string Username { get; private set; } = string.Empty;

        public Spotify.Profile SpotifyProfile { get; private set; } = Spotify.Profile.Empty;

        public void CompleteUserWitSpotifyProfile(Spotify.Profile profile)
        {
            Username = profile.UserName;
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
            SpotifyProfile = profile;
        }

        public void UpdateSpotifyProfile(Spotify.Profile profile) =>
            SpotifyProfile = profile;

        public static UserProfile Empty => new();

        public bool IsNewUser() => Username == string.Empty;
    }
}