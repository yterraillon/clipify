using Clipify.Application.Common.Interfaces;
using Clipify.Application.Spotify.Requests;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Clipify.Application.Common.Models;
using Newtonsoft.Json;

namespace Clipify.Infrastructure.Spotify
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private const string AuthorizeUri = "https://accounts.spotify.com/authorize";

        private string ClientId { get; set; } = "06e60e8e48db4378a95783a631ffbe60";

        private string ResponseType { get; set; } = "code";

        private string RedirectUri { get; set; } = "https://localhost:44389/spotify-auth";

        private string CodeChallengeMethod { get; set; } = "S256";

        private static string CodeVerifier { get; set; } = String.Empty;

        private string CodeChallenge { get; set; } = String.Empty;

        private HttpClient Client { get; } = LazyHttpClient.Value;

        private static string GenerateCodeVerifier()
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[120];

            random.GetBytes(bytes);

            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private string GenerateCodeChallenge()
        {
            try
            {
                using var sha256 = SHA256.Create();
                var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(CodeVerifier));

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

        public string GetAuthorizeUrl(SpotifyAuthorizeRequest.Request request)
        {
            CodeVerifier = GenerateCodeVerifier();
            CodeChallenge = GenerateCodeChallenge();

            var builder = new UriBuilder(AuthorizeUri);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query.Add("client_id", ClientId);
            query.Add("response_type", ResponseType);
            query.Add("redirect_uri", RedirectUri);
            query.Add("code_challenge_method", CodeChallengeMethod);
            query.Add("code_challenge", CodeChallenge);

            if (!string.IsNullOrEmpty(request.State))
                query.Add("state", request.State);

            if (!string.IsNullOrEmpty(request.Scope))
                query.Add("scope", request.Scope);

            builder.Query = query.ToString();

            return builder.ToString();
        }

        public async Task<SpotifyAuthResponse> GetAccessTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_id", ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", RedirectUri},
                {"code_verifier", CodeVerifier}
            };

            try
            {
                var response = await Client
                    .SendAsync(new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
                        {
                            Content = new FormUrlEncodedContent(parameters)
                        })
                    .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var content = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                if (content == null)
                    return new SpotifyAuthResponse(); // TODO: Error handling.

                return JsonConvert.DeserializeObject<SpotifyAuthResponse>(content, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    NullValueHandling = NullValueHandling.Ignore
                }) ?? new SpotifyAuthResponse(); // TODO: Error handling.
            }
            catch (HttpRequestException e)
            {
                // TODO: Error handling.
                Console.WriteLine(e);
                return new SpotifyAuthResponse();
            }

        }

        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>(() =>
        {
            var httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
            }, true);

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36");
            return httpClient;
        });
    }
}