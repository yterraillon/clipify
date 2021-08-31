using Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.SpotifyAuthentication.Commands;

namespace Infrastructure.Spotify.Authentication.Clients
{
    public class TokensClient : ISpotifyTokensClient
    {
        private readonly HttpClient _client;
        private readonly CodeProvider _codeProvider;

        private readonly Settings _settings;

        public TokensClient(HttpClient client, IOptions<Settings> settings, CodeProvider codeProvider)
        {
            _client = client;
            _codeProvider = codeProvider;
            _settings = settings.Value;
        }

        public async Task<(string accessToken, string refreshToken, int expiresIn)> GetAccessTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", _settings.AuthorizeRedirectUrl},
                {"code_verifier", _codeProvider.Verifier}
            };

            var tokens = await GetTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
            return (tokens.AccessToken, tokens.RefreshToken, tokens.ExpiresIn);
        }

        // TODO : vérifier que le refresh fonctionne correctement
        public async Task<(string accessToken, int expiresIn)> RefreshTokenAsync(string refreshToken)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };

            var tokens = await GetTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
            return (tokens.AccessToken, tokens.ExpiresIn);
        }

        private Task<TokensClientResponse> GetTokenAsync(Uri requestUri, IDictionary<string, string> parameters)
            => _client.PostRequestAsync<TokensClientResponse>(requestUri, HttpMethod.Post, parameters);
    }
}