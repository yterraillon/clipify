using System.Collections.Generic;
using Application.SpotifyAuthentication.Requests.GetAuthenticationUri;
using Application.SpotifyAuthentication.Requests.Login;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Authentication.AuthenticationUriBuilder
{
    public class AuthenticationUriBuilder : ISpotifyAuthenticationUriBuilder
    {
        private readonly CodeProvider _codeProvider;
        private readonly IStateProvider _stateProvider;
        private readonly Settings _settings;

        private const string ResponseType = "code";
        private const string CodeChallengeMethod = "S256";

        public AuthenticationUriBuilder(IOptions<Settings> settings, CodeProvider codeProvider, IStateProvider stateProvider)
        {
            _codeProvider = codeProvider;
            _stateProvider = stateProvider;
            _settings = settings.Value;
        }

        public string GetAuthorizeUrl(IEnumerable<string> scopes)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _settings.ClientId },
                { "response_type", ResponseType },
                { "redirect_uri", _settings.AuthorizeRedirectUrl },
                { "code_challenge_method", CodeChallengeMethod },
                { "code_challenge", _codeProvider.Challenge },
                { "scope", FormatScopes(scopes) },
                { "state", _stateProvider.State }
            };

            return QueryHelpers.AddQueryString(_settings.AuthorizeUrl, parameters);
        }

        private static string FormatScopes(IEnumerable<string> scopes) => string.Join(" ", scopes);
    }
}