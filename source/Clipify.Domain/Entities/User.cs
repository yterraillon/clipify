using System;

namespace Clipify.Domain.Entities
{
    public class User : Entity
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Avatar { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;


        public DateTime TokenExpirationDate { get; set; }

        public static User Empty => new();

        public static User Create(string userId, string username, string avatar, string accessToken, string refreshToken, int expiresIn)
            => new()
            {
                UserId = userId,
                Username = username,
                Avatar = avatar,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                TokenExpirationDate = DateTime.UtcNow.AddSeconds(expiresIn)
            };
    }
}