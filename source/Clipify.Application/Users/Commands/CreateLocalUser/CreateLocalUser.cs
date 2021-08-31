using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Domain.Entities;
using MediatR;
using System;
using System.Linq;
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

            public override string ToString() => 
                $"{nameof(AccessToken)}: {AccessToken}, {nameof(RefreshToken)}: {RefreshToken}, {nameof(ExpiresIn)}: {ExpiresIn}";
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

                var avatar = profile.Images
                                 .FirstOrDefault(x => !string.IsNullOrEmpty(x.Url))?.Url ?? string.Empty;
                var user = _userRepository.Get(x => x.UserId == profile.Id);

                if (string.IsNullOrEmpty(user.UserId))
                {
                    _userRepository.Add(User.Create(
                        profile.Id,
                        profile.DisplayName,
                        avatar,
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