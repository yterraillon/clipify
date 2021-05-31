using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Profile.Requests.GetProfile
{
    using Models;

    public interface IUserProfileClient
    {
        Task<ProfileResponse> GetUserProfileAsync(string token,
            CancellationToken cancellationToken = new CancellationToken());
    }
}