using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Authentication.Requests;
using Application.Authentication.Requests.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Authentication
{
    using Clients;

    public class SpotifyTokenService : ISpotifyTokenService
    {
        private readonly AuthenticationClient _client;
        private readonly CodeProvider _codeProvider;

        private readonly Settings.Settings _settings;

        public SpotifyTokenService(AuthenticationClient client, IOptions<Settings.Settings> settings, CodeProvider codeProvider)
        {
            _client = client;
            _codeProvider = codeProvider;
            _settings = settings.Value;
        }

        public Task<TokenResponse> GetAccessTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", _settings.AuthorizeRedirectUrl},
                {"code_verifier", _codeProvider.Verifier}
            };

            return _client.GetTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
        }

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };

            return _client.GetTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
        }
    }
}