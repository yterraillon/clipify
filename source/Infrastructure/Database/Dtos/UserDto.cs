using Domain.Entities;

namespace Infrastructure.Database.Dtos
{
    public class UserDto : EntityDto
    {
        public UserDto() => Id = UserProfile.DefaultUserId;

        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public SpotifyProfileDto SpotifyProfile { get; set; } = SpotifyProfileDto.Empty;

        public static UserDto Empty => new();
    }

    public class SpotifyProfileDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public static SpotifyProfileDto Empty => new();
    }
}