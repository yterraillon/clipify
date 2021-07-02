using Clipify.Application.Auth.Requests.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Clipify.Infrastructure.SpotifyAuth
{
    using Settings;

    public class SpotifyAuthUriBuilder : IAuthUriBuilder
    {
        private readonly SpotifyAuthSettings _settings;

        private const string ResponseType = "code";

        private const string CodeChallengeMethod = "S256";

        public SpotifyAuthUriBuilder(IOptions<SpotifyAuthSettings> settings) => _settings = settings.Value;

        public string GetAuthorizeUrl(string challenge, string scope, string state)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _settings.ClientId },
                { "response_type", ResponseType },
                { "redirect_uri", _settings.AuthorizeRedirectUrl },
                { "code_challenge_method", CodeChallengeMethod },
                { "code_challenge", challenge }
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