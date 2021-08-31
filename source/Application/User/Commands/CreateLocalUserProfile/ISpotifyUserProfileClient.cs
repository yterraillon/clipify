using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.User.Commands.CreateLocalUserProfile
{
    public interface ISpotifyUserProfileClient
    {
        Task<ServiceProfile> GetUserProfile(string token, CancellationToken cancellationToken);
    }
}