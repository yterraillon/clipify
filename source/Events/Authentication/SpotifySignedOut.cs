using MediatR;

namespace Events.Authentication
{
    public record SpotifySignedOut : INotification;
}