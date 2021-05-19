using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using MediatR;

namespace Clipify.Application.Spotify.Requests.ProfileRequest
{
    public static class Profile
    {
        public class Request : IRequest<ProfileResponse>
        {

        }

        public class Handler : IRequestHandler<Request, ProfileResponse>
        {
            public Task<ProfileResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}