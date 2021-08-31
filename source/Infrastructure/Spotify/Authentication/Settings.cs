﻿namespace Infrastructure.Spotify.Authentication
{
    public class Settings
    {
        public string ClientId { get; set; } = string.Empty;

        public string AuthorizeUrl { get; set; } = string.Empty;

        public string AccessTokenUrl { get; set; } = string.Empty;

        public string AuthorizeRedirectUrl { get; set; } = string.Empty;

        public string AccessTokenRedirectUrl { get; set; } = string.Empty;
    }
}