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
                .ForMember(d => d.Tracks,
                    o => o.MapFrom(s => s.Tracks.Items.Select(t => t.Track)));
            CreateMap<TrackResponse, TrackViewModel>();
        }
    }
}