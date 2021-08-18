using Domain.Spotify;
using MediatR;

namespace Events.Authentication
{
    public record LoggedIntoSpotify : INotification
    {
        public Tokens Tokens { get; set; } = Tokens.Empty;
    }
}