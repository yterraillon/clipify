using MediatR;

namespace Events.Playlists
{
    public record PlaylistUpdated(string PlaylistId) : INotification;
}