using System.Collections.Generic;
using Application.Authentication.Requests.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Authentication
{
    public class AuthenticationUriBuilder : ISpotifyAuthenticationUriBuilder
    {
        private readonly CodeProvider _codeProvider;
        private readonly Settings.Settings _settings;

        private const string ResponseType = "code";

        private const string CodeChallengeMethod = "S256";

        public AuthenticationUriBuilder(IOptions<Settings.Settings> settings, CodeProvider codeProvider)
        {
            _codeProvider = codeProvider;
            _settings = settings.Value;
        }

        public string GetAuthorizeUrl(string scope, string state)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _settings.ClientId },
                { "response_type", ResponseType },
                { "redirect_uri", _settings.AuthorizeRedirectUrl },
                { "code_challenge_method", CodeChallengeMethod },
                { "code_challenge", _codeProvider.Challenge }
            };

            if (!string.IsNullOrEmpty(state))
                parameters.Add("state", state);

            BuildScopes(parameters, scope);

            return QueryHelpers.AddQueryString(_settings.AuthorizeUrl, parameters);
        }

        private static void BuildScopes(IDictionary<string, string> parameters, string scope)
        {
            if (!string.IsNullOrEmpty(scope))
                parameters.Add("scope", scope);

            var scopes = string.Join(" ",
                SpotifyScopes.PlaylistReadPrivate,
                SpotifyScopes.PlaylistReadCollaborative,
                SpotifyScopes.UserReadPrivate);

            if (parameters.ContainsKey("scope"))
                parameters["scope"] += scopes;
            else
                parameters.Add("scope", scopes);
        }
    }
}