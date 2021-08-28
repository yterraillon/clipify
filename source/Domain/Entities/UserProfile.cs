using System;

namespace Domain.Entities
{
    using static Constants;

    public class UserProfile : UniqueEntity
    {
        public const string DefaultUserId = "default";
        public UserProfile() : base(DefaultUserId) { }

        public string Username { get; private set; } = string.Empty;
        public ServiceProfile SpotifyServiceProfile { get; private set; } = ServiceProfile.Empty(Services.Spotify);

        public void CreateUserWitSpotifyProfile(ServiceProfile serviceProfile)
        {
            Username = serviceProfile.UserName;
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
            SpotifyServiceProfile = serviceProfile;
        }

        public void UpdateSpotifyProfile(ServiceProfile serviceProfile)
        {
            SpotifyServiceProfile = serviceProfile;
            Updated = DateTime.UtcNow;
        }

        public bool IsNewUser() => Username == string.Empty;

        public static UserProfile Empty => new();
    }
}