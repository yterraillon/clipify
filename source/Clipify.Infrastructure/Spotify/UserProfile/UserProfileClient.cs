using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Infrastructure.Extensions;
using Clipify.Infrastructure.Spotify.Settings;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Clipify.Infrastructure.Spotify.UserProfile
{
    public class UserProfileClient : IUserProfileClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<UserProfileClient> _logger;

        private readonly SpotifyApiSettings _apiSettings;

        public UserProfileClient(HttpClient client, IOptions<SpotifyApiSettings> apiSettings, ILogger<UserProfileClient> logger)
        {
            _client = client;
            _logger = logger;
            _apiSettings = apiSettings.Value;
        }

        public async Task<ProfileResponse> GetUserProfileAsync(string token, CancellationToken cancellationToken)
        {
            try
            {
                return await _client.ConfigureAuthorization(token)
                    .PostRequestAsync<ProfileResponse>(
                        new Uri(_apiSettings.BaseUrl + _apiSettings.ProfileEndpoint),
                        HttpMethod.Get,
                        cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ProfileResponse.Empty;
            }
        }
    }
}