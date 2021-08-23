using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Events.Authentication;
using Events.User;
using MediatR;

namespace Application.User.Commands.CreateLocalUserProfile
{
    using Domain.Entities;

    public static class CreateLocalUserProfile
    {
        public class Handler : INotificationHandler<LoggedInWithSpotify>
        {
            private readonly IRepository<UserProfile, Guid> _userRepository;
            private readonly ISpotifyUserProfileClient _spotifyUserProfileClient;
            private readonly IEventBus _eventBus;

            public Handler(IRepository<UserProfile, Guid> userRepository, ISpotifyUserProfileClient spotifyUserProfileClient, IEventBus eventBus)
            {
                _userRepository = userRepository;
                _spotifyUserProfileClient = spotifyUserProfileClient;
                _eventBus = eventBus;
            }

            public async Task Handle(LoggedInWithSpotify notification, CancellationToken cancellationToken)
            {
                var user = _userRepository.Get(Constants.DefaultId);

                var spotifyProfile =
                    await _spotifyUserProfileClient.GetUserProfile(notification.AccessToken, cancellationToken);

                if (user.IsNewUser())
                {
                    user.CompleteUserWitSpotifyProfile(spotifyProfile);
                    _userRepository.Add(user);
                }
                else
                {
                    user.UpdateSpotifyProfile(spotifyProfile);
                    _userRepository.Update(user);
                }

                await _eventBus.Publish(new SpotifyUserProfileUpdated(spotifyProfile.Id));
            }
        }
    }
}