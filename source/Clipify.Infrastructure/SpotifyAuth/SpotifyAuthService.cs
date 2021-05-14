using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Clipify.Infrastructure.SpotifyAuth
{
    public class SpotifyAuthService : IAuthService
    {
        private const string ClientId = "06e60e8e48db4378a95783a631ffbe60";

        private SpotifyAuthClient Client { get; }

        private SpotifyAuthSettings Settings { get; }

        public SpotifyAuthService(SpotifyAuthClient client, IOptions<SpotifyAuthSettings> settings)
        {
            Client = client;
            Settings = settings.Value;
        }

        public Task<AccessTokenResponse> GetAccessTokenAsync(string verifier, string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", Settings.AuthorizeRedirectUrl},
                {"code_verifier", verifier}
            };

            return Client.GetAccessTokenAsync(new Uri(Settings.AccessTokenUrl), parameters);
        }
    }
}