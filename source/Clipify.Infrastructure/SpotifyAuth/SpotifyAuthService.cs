using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
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
        private const string ClientId = "06e60e8e48db4378a95783a631ffbe60";

        private readonly SpotifyAuthClient _client;

        private readonly SpotifyAuthSettings _settings;

        public SpotifyAuthService(SpotifyAuthClient client, IOptions<SpotifyAuthSettings> settings)
        {
            _client = client;
            _settings = settings.Value;
        }

        public Task<AccessTokenResponse> GetAccessTokenAsync(string verifier, string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", _settings.AuthorizeRedirectUrl},
                {"code_verifier", verifier}
            };

            return _client.GetAccessTokenAsync(new Uri(_settings.AccessTokenUrl), parameters);
        }
    }
}