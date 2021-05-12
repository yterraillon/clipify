using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using Clipify.Application.Auth.Requests.AuthorizeRequest;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Clipify.Infrastructure.SpotifyAuth.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.SpotifyAuth
{
    public class SpotifyAuthService : IAuthService
    {
        private const string ClientId = "06e60e8e48db4378a95783a631ffbe60";

        private string CodeVerifier { get; set; } = String.Empty;

        private string CodeChallenge { get; set; } = String.Empty;

        private SpotifyAuthClient Client { get; }

        private SpotifyAuthSettings Settings { get; }

        public SpotifyAuthService(SpotifyAuthClient client, IOptions<SpotifyAuthSettings> settings)
        {
            Client = client;
            Settings = settings.Value;
        }

        private static string GenerateCodeVerifier()
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[120];

            random.GetBytes(bytes);

            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private string GenerateCodeChallenge()
        {
            try
            {
                using var sha256 = SHA256.Create();
                var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(CodeVerifier));

                return Convert.ToBase64String(codeChallengeBytes)
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');
            }
            catch (Exception e)
            {
                // NOTE: Better logging/exception handling?
                Console.WriteLine(e);

                return string.Empty;
            }
        }

        public string GetAuthorizeUrl(Authorization.Request request)
        {
            CodeVerifier = GenerateCodeVerifier();
            CodeChallenge = GenerateCodeChallenge();

            return string.Empty;
        }

        public Task<AccessTokenResponse> GetAccessTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", Settings.RedirectUrl},
                {"code_verifier", CodeVerifier}
            };

            return Client.GetAccessTokenAsync(new Uri(Settings.AccessTokenUrl), parameters);
        }
    }
}