using Clipify.Application.Auth.Requests;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Clipify.Infrastructure.SpotifyAuth
{
    public class SpotifyAuthCodeProvider : IAuthCodeProvider
    {
        public string Verifier { get; }

        public string Challenge { get; } = string.Empty;

        private static readonly char[] Characters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
        };

        public SpotifyAuthCodeProvider()
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

            try
            {
                using var sha256 = SHA256.Create();
                var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Verifier));

                Challenge = SafeToBase64String(codeChallengeBytes);
            }
            catch (Exception e)
            {
                // NOTE: Better logging/exception handling?
                Console.WriteLine(e);
            }
        }

        private static string SafeToBase64String(byte[] bytes) =>
            Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
    }
}