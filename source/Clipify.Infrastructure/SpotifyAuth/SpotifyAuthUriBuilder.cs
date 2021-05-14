using Clipify.Application.Auth.Requests;
using Clipify.Infrastructure.SpotifyAuth.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Clipify.Infrastructure.SpotifyAuth
{
    public class SpotifyAuthUriBuilder : IAuthUriBuilder
    {
        private SpotifyAuthSettings Settings { get; }

        private const string ClientId = "06e60e8e48db4378a95783a631ffbe60"; // TODO: Get from github secret

        private const string ResponseType = "code";

        private const string CodeChallengeMethod = "S256";

        public SpotifyAuthUriBuilder(IOptions<SpotifyAuthSettings> settings)
        {
            Settings = settings.Value;
        }

        public string GetAuthorizeUrl(string challenge, string scope, string state)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "response_type", ResponseType },
                { "redirect_uri", Settings.AuthorizeRedirectUrl },
                { "code_challenge_method", CodeChallengeMethod },
                { "code_challenge", challenge }
            };

            if (!string.IsNullOrEmpty(scope))
                parameters.Add("scope", scope);
            if (!string.IsNullOrEmpty(state))
                parameters.Add("state", state);

            return QueryHelpers.AddQueryString(Settings.AuthorizeUrl, parameters);
        }
    }
}