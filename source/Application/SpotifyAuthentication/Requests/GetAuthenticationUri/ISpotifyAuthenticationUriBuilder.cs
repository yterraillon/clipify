using System.Collections.Generic;

namespace Application.SpotifyAuthentication.Requests.GetAuthenticationUri
{
    public interface ISpotifyAuthenticationUriBuilder
    {
        public string GetAuthorizeUrl(IEnumerable<string> scopes);
    }
}