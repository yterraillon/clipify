using System.Threading;
using System.Threading.Tasks;
using Events.Authentication;
using MediatR;

namespace Application.User.Commands.CreateLocalSpotifyUserProfile
{
    public static class CreateLocalSpotifyUserProfile
    {
        public class Handler : INotificationHandler<LoggedIntoSpotify>
        {
            private readonly IRepository<Domain.Entities.User, string> _userRepository;
            private readonly ISpotifyUserProfileClient _spotifyUserProfileClient;

            public Handler(IRepository<Domain.Entities.User, string> userRepository, ISpotifyUserProfileClient spotifyUserProfileClient)
            {
                _userRepository = userRepository;
                _spotifyUserProfileClient = spotifyUserProfileClient;
            }

            public Task Handle(LoggedIntoSpotify notification, CancellationToken cancellationToken)
            {
                // Get profile if exists, otherwise create it
                // Add tokens to it
                // store it
                throw new System.NotImplementedException();
            }
        }
    }
}