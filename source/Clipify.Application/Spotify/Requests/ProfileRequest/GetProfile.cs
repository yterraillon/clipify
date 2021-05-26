﻿using Clipify.Application.Spotify.Clients;
using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using Clipify.Application.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Spotify.Requests.ProfileRequest
{
    public static class GetProfile
    {
        public class Request : IRequest<ProfileResponse>
        {

        }

        public class Handler : IRequestHandler<Request, ProfileResponse>
        {
            private readonly ISpotifyClient _client;
            private readonly ICurrentUserService _currentUser;

            public Handler(ISpotifyClient client, ICurrentUserService currentUser)
            {
                _client = client;
                _currentUser = currentUser;
            }

            public Task<ProfileResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = _currentUser.GetCurrentUser()?.AccessToken;

                return string.IsNullOrEmpty(token)
                    ? Task.FromResult(ProfileResponse.Empty)
                    : _client.GetUserProfileAsync(token, cancellationToken);
            }
        }
    }
}