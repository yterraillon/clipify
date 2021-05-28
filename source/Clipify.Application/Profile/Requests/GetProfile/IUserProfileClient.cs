using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Profile.Requests.GetProfile.Models;

namespace Clipify.Application.Profile.Requests.GetProfile
{
    public interface IUserProfileClient
    {
        Task<ProfileResponse> GetUserProfileAsync(string token,
            CancellationToken cancellationToken = new CancellationToken());
    }
}