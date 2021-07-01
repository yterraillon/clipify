using System;

namespace Clipify.Infrastructure.Database.Dtos
{
    public class UserDto : EntityDto
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenExpirationDate { get; set; }

        public static UserDto Empty => new();
    }
}