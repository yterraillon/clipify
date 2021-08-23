using MediatR;

namespace Events.User
{
    public record SpotifyUserProfileUpdated(string UserId) : INotification;
}