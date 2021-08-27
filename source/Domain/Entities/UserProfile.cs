using System;

namespace Domain.Entities
{
    public class UserProfile : UniqueEntity
    {
        public const string DefaultUserId = "default";
        public UserProfile() : base(DefaultUserId) { }

        public string Username { get; private set; } = string.Empty;
        public Spotify.Profile SpotifyProfile { get; private set; } = Spotify.Profile.Empty;

        public void CreateUserWitSpotifyProfile(Spotify.Profile profile)
        {
            Username = profile.UserName;
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
            SpotifyProfile = profile;
        }

        public void UpdateSpotifyProfile(Spotify.Profile profile)
        {
            SpotifyProfile = profile;
            Updated = DateTime.UtcNow;
        }

        public bool IsNewUser() => Username == string.Empty;

        public static UserProfile Empty => new();
    }
}