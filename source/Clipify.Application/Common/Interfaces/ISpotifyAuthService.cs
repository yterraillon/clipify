using System.Threading.Tasks;
using Clipify.Application.Spotify.Requests;

namespace Clipify.Application.Common.Interfaces
{
    public interface ISpotifyAuthService
    {
        public string GetAuthorizeUrl(SpotifyAuthorizeRequest.Request request);
    }
}