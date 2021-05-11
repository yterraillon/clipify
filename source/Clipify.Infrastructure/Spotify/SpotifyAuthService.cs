using Clipify.Application.Common.Interfaces;
using Clipify.Application.Spotify.Requests;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Clipify.Infrastructure.Spotify
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private const string AuthorizeUri = "https://accounts.spotify.com/authorize";

        private string ClientId { get; set; } = "06e60e8e48db4378a95783a631ffbe60";

        private string ResponseType { get; set; } = "code";

        private string RedirectUri { get; set; } = "https://localhost:44389/spotify-auth";

        private string CodeChallengeMethod { get; set; } = "S256";

        private string CodeVerifier { get; set; } = String.Empty;

        private string CodeChallenge { get; set; } = String.Empty;

        private static string GenerateCodeVerifier()
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[128];

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

        public string GetAuthorizeUrl(SpotifyAuthorizeRequest.Request request)
        {
            CodeVerifier = GenerateCodeVerifier();
            CodeChallenge = GenerateCodeChallenge();

            var builder = new UriBuilder(AuthorizeUri);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query.Add("client_id", ClientId);
            query.Add("response_type", ResponseType);
            query.Add("redirect_uri", RedirectUri);
            query.Add("code_challenge_method", CodeChallengeMethod);
            query.Add("code_challenge", CodeChallenge);

            if (!string.IsNullOrEmpty(request.State))
                query.Add("state", request.State);

            if (!string.IsNullOrEmpty(request.Scope))
                query.Add("scope", request.Scope);

            builder.Query = query.ToString();

            return builder.ToString();
        }
    }
}