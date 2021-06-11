﻿namespace Clipify.Domain.Entities
{
    public class User : Entity
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public int ExpiresIn { get; set; }

        public static User Empty => new User();
    }
}