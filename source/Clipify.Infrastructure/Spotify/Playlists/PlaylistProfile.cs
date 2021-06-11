using AutoMapper;
using Clipify.Application.Playlists.Models;
using Clipify.Infrastructure.Spotify.Playlists.Models;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<PlaylistImageResponse, PlaylistImageViewModel>();
            CreateMap<PlaylistResponse, PlaylistViewModel>();
        }
    }
}