using System;

namespace Clipify.Domain.Entities
{
    public class User : Entity
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenExpirationDate { get; set; }

        public static User Empty => new User();

        public static User Create(string userId, string username, string accessToken, string refreshToken,
            int expiresIn) => new User
        {
                UserId = userId,
                Username = username,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                TokenExpirationDate = DateTime.UtcNow.AddSeconds(expiresIn)
        };
    }
}