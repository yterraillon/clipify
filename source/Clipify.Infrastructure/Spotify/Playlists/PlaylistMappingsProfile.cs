using AutoMapper;
using Clipify.Application.Playlists.Models;
using Clipify.Infrastructure.Spotify.Models.Playlist;
using Clipify.Infrastructure.Spotify.Models.Track;
using System.Linq;

namespace Clipify.Infrastructure.Spotify.Playlists
{
    public class PlaylistMappingsProfile : Profile
    {
        public PlaylistMappingsProfile()
        {
            CreateMap<PlaylistImageResponse, PlaylistImageViewModel>();
            CreateMap<PlaylistResponse, PlaylistViewModel>();
            CreateMap<PlaylistWithTracksResponse, PlaylistViewModel>()
                .ForMember(dest => dest.Tracks,
                    opt => opt.MapFrom(src => src.Tracks.Items.Select(item => item.Track)));
            CreateMap<TrackResponse, TrackViewModel>();
            CreateMap<Artist, ArtistViewModel>();
            CreateMap<Album, AlbumViewModel>();
        }
    }
}