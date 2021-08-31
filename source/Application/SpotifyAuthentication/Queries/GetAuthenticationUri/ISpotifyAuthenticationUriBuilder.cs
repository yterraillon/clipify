using System.Collections.Generic;

namespace Application.SpotifyAuthentication.Queries.GetAuthenticationUri
{
    public interface ISpotifyAuthenticationUriBuilder
    {
        public string GetAuthorizeUrl(IEnumerable<string> scopes);
    }
}