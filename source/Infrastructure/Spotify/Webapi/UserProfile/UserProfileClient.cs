using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.User.Commands.CreateLocalUserProfile;
using Domain;
using Infrastructure.Extensions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Webapi.UserProfile
{
    using static Constants;

    public class UserProfileClient : ISpotifyUserProfileClient
    {
        private readonly HttpClient _client;
        private readonly Settings _settings;

        public UserProfileClient(HttpClient client, IOptions<Settings> webapiSettings)
        {
            _client = client;
            _settings = webapiSettings.Value;
        }

        public async Task<ServiceProfile> GetUserProfile(string token, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _client.ConfigureAuthorization(token)
                    .PostRequestAsync<UserProfileResponse>(
                        new Uri(_settings.BaseUrl + _settings.ProfileEndpoint),
                        HttpMethod.Get,
                        cancellationToken: cancellationToken);

                return new ServiceProfile
                {
                    ServiceName = Services.Spotify,
                    UserName = response.DisplayName,
                    Id = response.UserId
                };
            }
            catch (Exception)
            {
                return ServiceProfile.Empty(Services.Spotify);
            }
        }
    }
}