using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Infrastructure.Extensions;
using Clipify.Infrastructure.Spotify.Settings;

namespace Clipify.Infrastructure.Spotify.UserProfile
{
    public class UserProfileClient : IUserProfileClient
    {
        private readonly HttpClient _client;

        private readonly SpotifyApiSettings _apiSettings;

        public UserProfileClient(HttpClient client, SpotifyApiSettings apiSettings)
        {
            _client = client;
            _apiSettings = apiSettings;
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