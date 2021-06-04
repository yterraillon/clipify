using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Infrastructure.Extensions;

namespace Clipify.Infrastructure.Spotify.UserProfile
{
    public class UserProfileClient : IUserProfileClient
    {
        private readonly HttpClient _client;

        public UserProfileClient(HttpClient client)
        {
            _client = client;
        }

        public Task<ProfileResponse> GetUserProfileAsync(string token, CancellationToken cancellationToken)
        {
            return _client.ConfigureAuthorization(token)
                .PostRequestAsync<ProfileResponse>(
                    new Uri("https://api.spotify.com/v1/me"),
                    HttpMethod.Get,
                    cancellationToken: cancellationToken);
        }
    }
}