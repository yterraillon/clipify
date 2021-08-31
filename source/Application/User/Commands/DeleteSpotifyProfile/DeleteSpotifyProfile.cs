using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Events.Authentication;
using MediatR;

namespace Application.User.Commands.DeleteSpotifyProfile
{
    public static class DeleteSpotifyProfile
    {
        public class Handler : INotificationHandler<SpotifySignedOut>
        {
            private readonly IRepository<UserProfile> _userProfileRepository;

            public Handler(IRepository<UserProfile> userProfileRepository) => _userProfileRepository = userProfileRepository;

            public Task Handle(SpotifySignedOut notification, CancellationToken cancellationToken)
            {
                var userProfile = _userProfileRepository.Get(UserProfile.DefaultUserId);
                userProfile.UpdateSpotifyProfile(ServiceProfile.Empty());
                _userProfileRepository.Update(userProfile);

                return Task.CompletedTask;
            }
        }
    }
}