using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.SpotifyAuthentication.Requests;
using Domain.Spotify;
using Infrastructure.Extensions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Authentication.Clients
{
    public class TokenServiceClient : ISpotifyTokenService
    {
        private readonly HttpClient _client;
        private readonly CodeProvider _codeProvider;

        private readonly Settings _settings;

        public TokenServiceClient(HttpClient client, IOptions<Settings> settings, CodeProvider codeProvider)
        {
            _client = client;
            _codeProvider = codeProvider;
            _settings = settings.Value;
        }

        public async Task<Tokens> GetAccessTokenAsync(string code)
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
            return ToSpotifyTokens(tokens);
        }

        public async Task<Tokens> RefreshTokenAsync(string refreshToken)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", _settings.ClientId},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };

            var tokens = await GetTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
            return ToSpotifyTokens(tokens);
        }

        private Task<TokenServiceResponse> GetTokenAsync(Uri requestUri, IDictionary<string, string> parameters)
            => _client.PostRequestAsync<TokenServiceResponse>(requestUri, HttpMethod.Post, parameters);

        private static Tokens ToSpotifyTokens(TokenServiceResponse tokens) =>
            new (tokens.AccessToken, tokens.RefreshToken, tokens.ExpiresIn);
    }
}