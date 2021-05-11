using Clipify.Application.Common.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Clipify.Infrastructure.Spotify
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        public string GenerateCodeVerifier()
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[128];

            random.GetBytes(bytes);

            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public string GenerateCodeChallenge(string codeVerifier)
        {
            try
            {
                using var sha256 = SHA256.Create();
                var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));

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
    }
}