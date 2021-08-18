using System.Threading;
using System.Threading.Tasks;
using Domain.Spotify;

namespace Application.User.Commands.CreateLocalSpotifyUserProfile
{
    public interface ISpotifyUserProfileClient
    {
        Task<Profile> GetUserProfile(string token, CancellationToken cancellationToken);
    }
}