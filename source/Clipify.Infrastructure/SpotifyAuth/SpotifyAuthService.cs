﻿using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.TokenRequest.Models;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.SpotifyAuth
{
    public class SpotifyAuthService : IAuthService
    {
        private readonly SpotifyAuthClient _client;

        private readonly SpotifyAuthSettings _settings;

        public SpotifyAuthService(SpotifyAuthClient client, IOptions<SpotifyAuthSettings> settings)
        {
            _client = client;
            _settings = settings.Value;
        }

        public Task<TokenResponse> GetAccessTokenAsync(string verifier, string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", _settings.AuthorizeRedirectUrl},
                {"code_verifier", verifier}
            };

            return _client.GetTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
        }

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };

            return _client.GetTokenAsync(new Uri("https://accounts.spotify.com/api/token"), parameters);
        }
    }
}