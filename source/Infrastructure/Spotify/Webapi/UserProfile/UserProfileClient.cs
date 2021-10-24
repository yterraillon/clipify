﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.User.Commands.CreateLocalUserProfile;
using Domain;
using Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Spotify.Webapi.UserProfile
{
    public class UserProfileClient : ISpotifyUserProfileClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<UserProfileClient> _logger;
        private readonly Settings _settings;

        public UserProfileClient(HttpClient client, IOptions<Settings> webapiSettings, ILogger<UserProfileClient> logger)
        {
            _client = client;
            _logger = logger;
            _settings = webapiSettings.Value;
        }

        public async Task<ServiceProfile> GetUserProfile(string token, CancellationToken cancellationToken)
        {
            _client.ConfigureAuthorizationHeader(token);

            try
            {
                var response = await _client.PostRequestAsync<PrivateUserObject>(
                        new Uri(_settings.BaseUrl + _settings.ProfileEndpoint),
                        HttpMethod.Get,
                        cancellationToken: cancellationToken);

                return new ServiceProfile
                {
                    UserName = response.DisplayName,
                    Id = response.UserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ServiceProfile.Empty();
            }
        }
    }
}