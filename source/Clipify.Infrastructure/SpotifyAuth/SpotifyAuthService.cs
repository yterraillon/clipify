using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using Clipify.Application.Auth.Requests.AuthorizeRequest;
using Clipify.Infrastructure.SpotifyAuth.Clients;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Clipify.Infrastructure.SpotifyAuth
{
    public class SpotifyAuthService : IAuthService
    {
        public class Settings
        {
            public string AuthorizeUrl { get; set; } = string.Empty;

            public string AccessTokenUrl { get; set; } = string.Empty;

            public string RedirectUrl { get; set; } = string.Empty;
        }

        private string ClientId { get; } = "06e60e8e48db4378a95783a631ffbe60";

        private const string ResponseType = "code";

        private const string CodeChallengeMethod = "S256";

        private string CodeVerifier { get; set; } = String.Empty;

        private string CodeChallenge { get; set; } = String.Empty;

        private SpotifyAuthClient Client { get; }

        private Settings SpotifySettings { get; }

        public SpotifyAuthService(SpotifyAuthClient client, IOptions<Settings> spotifySettings)
        {
            Client = client;
            SpotifySettings = spotifySettings.Value;
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

            var builder = new UriBuilder(SpotifySettings.AuthorizeUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query.Add("client_id", ClientId);
            query.Add("response_type", ResponseType);
            query.Add("redirect_uri", SpotifySettings.RedirectUrl);
            query.Add("code_challenge_method", CodeChallengeMethod);
            query.Add("code_challenge", CodeChallenge);

            if (!string.IsNullOrEmpty(request.State))
                query.Add("state", request.State);

            if (!string.IsNullOrEmpty(request.Scope))
                query.Add("scope", request.Scope);

            builder.Query = query.ToString();

            return builder.ToString();
        }

        public Task<AccessTokenResponse> GetAccessTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", SpotifySettings.RedirectUrl},
                {"code_verifier", CodeVerifier}
            };

            return Client.GetAccessTokenAsync(new Uri(SpotifySettings.AccessTokenUrl), parameters);
        }
    }
}