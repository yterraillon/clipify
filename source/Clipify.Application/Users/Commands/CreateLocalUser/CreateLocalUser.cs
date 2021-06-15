using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Users.Commands.CreateLocalUser
{
    public static class CreateLocalUser
    {
        public class Command : IRequest
        {
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;

            public int ExpiresIn { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IRepository<User, string> _userRepository;

            private readonly IUserProfileClient _userProfileClient;

            public Handler(IRepository<User, string> userRepository, IUserProfileClient userProfileClient)
            {
                _userRepository = userRepository;
                _userProfileClient = userProfileClient;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await _userProfileClient.GetUserProfileAsync(request.AccessToken, cancellationToken);

                if (profile.Equals(ProfileResponse.Empty))
                    return;

                var user = _userRepository.Get(x => x.UserId == profile.Id);

                if (string.IsNullOrEmpty(user.UserId))
                {
                    _userRepository.Add(User.Create(
                        profile.Id,
                        profile.DisplayName,
                        request.AccessToken,
                        request.RefreshToken,
                        request.ExpiresIn
                    ));
                }
                else
                {
                    user.AccessToken = request.AccessToken;
                    user.RefreshToken = request.RefreshToken;
                    user.TokenExpirationDate = DateTime.UtcNow.AddSeconds(request.ExpiresIn);

                    _userRepository.Update(user);
                }
            }
        }
    }
}