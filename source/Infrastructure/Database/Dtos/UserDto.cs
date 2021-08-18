using System;

namespace Infrastructure.Database.Dtos
{
    public class UserDto : EntityDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public SpotifyProfileDto SpotifyProfile { get; set; } = SpotifyProfileDto.Empty;

        public static UserDto Empty => new();
    }

    public class SpotifyProfileDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        //public SpotifyTokensDto Tokens { get; set; } = SpotifyTokensDto.Empty;

        public static SpotifyProfileDto Empty => new();
    }

    public class SpotifyTokensDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string RefreshToken { get; set; } = string.Empty;

        public static SpotifyTokensDto Empty => new();
    }
}