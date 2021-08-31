using System;

namespace Domain.Entities
{
    public class UserProfile : UniqueEntity
    {
        public const string DefaultUserId = "default";
        public UserProfile() : base(DefaultUserId) { }

        public string Username { get; private set; } = string.Empty;
        public ServiceProfile SpotifyServiceProfile { get; private set; } = ServiceProfile.Empty();

        public void CreateUserWitSpotifyProfile(ServiceProfile serviceProfile)
        {
            Username = serviceProfile.UserName;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            SpotifyServiceProfile = serviceProfile;
        }

        public void UpdateSpotifyProfile(ServiceProfile serviceProfile)
        {
            SpotifyServiceProfile = serviceProfile;
            Updated = DateTime.Now;
        }

        public bool IsNewUser() => Username == string.Empty;

        public static UserProfile Empty => new();
    }
}