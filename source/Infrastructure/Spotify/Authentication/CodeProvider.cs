using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Spotify.Authentication
{
    public class CodeProvider
    {
        private readonly ILogger<CodeProvider> _logger;
        public string Verifier { get; private set; } = string.Empty;

        public string Challenge { get; private set; } = string.Empty;


        private static readonly char[] Characters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
        };

        public CodeProvider(ILogger<CodeProvider> logger)
        {
            _logger = logger;
            BuildVerifier();
            BuildChallenge();
        }

        private void BuildVerifier()
        {
            var code = new char[128];
            var secureBytes = new byte[128];

            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(secureBytes);

            for (var i = 0; i < code.Length; i++)
            {
                var charIndex = secureBytes[i] % Characters.Length;
                code[i] = Characters[charIndex];
            }

            Verifier = new string(code);
        }

        private void BuildChallenge()
        {
            try
            {
                using var sha256 = SHA256.Create();
                var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Verifier));

                Challenge = SafeToBase64String(codeChallengeBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        private static string SafeToBase64String(byte[] bytes) =>
            Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
    }
}