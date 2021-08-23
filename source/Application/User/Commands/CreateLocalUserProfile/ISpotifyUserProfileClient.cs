using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Spotify;

namespace Application.User.Commands.CreateLocalUserProfile
{
    public interface ISpotifyUserProfileClient
    {
        Task<Profile> GetUserProfile(string token, CancellationToken cancellationToken);
    }
}