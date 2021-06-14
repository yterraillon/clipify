using AutoMapper;
using Clipify.Application.Playlists.Models;
using Clipify.Infrastructure.Spotify.Playlists.Models;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistMappingsProfile : Profile
    {
        public PlaylistMappingsProfile()
        {
            CreateMap<PlaylistImageResponse, PlaylistImageViewModel>();
            CreateMap<PlaylistResponse, PlaylistViewModel>();
        }
    }
}