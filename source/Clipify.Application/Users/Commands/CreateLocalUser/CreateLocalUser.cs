using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Profile.Requests.GetProfile;
using Clipify.Application.Profile.Requests.GetProfile.Models;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Users.Commands.CreateLocalUser
{
    public static class CreateLocalUser
    {
        public class Command : IRequest<Unit>
        {
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;

            public int ExpiresIn { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IRepository<User, string> _userRepository;

            private readonly IUserProfileClient _userProfileClient;

            public Handler(IRepository<User, string> userRepository, IUserProfileClient userProfileClient)
            {
                _userRepository = userRepository;
                _userProfileClient = userProfileClient;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await _userProfileClient.GetUserProfileAsync(request.AccessToken, cancellationToken);

                if (profile.Equals(ProfileResponse.Empty))
                    return Unit.Value;

                var user = _userRepository.Get(x => x.UserId == profile.Id);

                if (string.IsNullOrEmpty(user.UserId))
                {
                    _userRepository.Add(new User
                    {
                        UserId = profile.Id,
                        Username = profile.DisplayName,
                        AccessToken = request.AccessToken,
                        RefreshToken = request.RefreshToken,
                        ExpiresIn = request.ExpiresIn
                    });
                }

                return Unit.Value;
            }
        }
    }
}