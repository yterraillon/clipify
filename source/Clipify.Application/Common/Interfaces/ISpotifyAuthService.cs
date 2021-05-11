namespace Clipify.Application.Common.Interfaces
{
    public interface ISpotifyAuthService
    {
        public string GenerateCodeVerifier();

        public string GenerateCodeChallenge(string codeVerifier);
    }
}