﻿using Newtonsoft.Json;

namespace Application.Authentication.Requests.Models
{
    // TODO : should Be Spotify Tokens and stored in their table
    // Put Json mapping in infra
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonProperty("scope")]
        public string Scope { get; set; } = string.Empty;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        public static TokenResponse Empty => new();
    }
}