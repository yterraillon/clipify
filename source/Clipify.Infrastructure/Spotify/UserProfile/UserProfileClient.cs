using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Infrastructure.Extensions;
using Clipify.Infrastructure.Spotify.Settings;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Clipify.Infrastructure.Spotify.UserProfile
{
    public class UserProfileClient : IUserProfileClient
    {
        private readonly HttpClient _client;

        private readonly SpotifyApiSettings _apiSettings;

        public UserProfileClient(HttpClient client, IOptions<SpotifyApiSettings> apiSettings)
        {
            _client = client;
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
            catch (Exception)
            {
                return ProfileResponse.Empty;
            }
        }
    }
}