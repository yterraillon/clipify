using Application.Playlists.Queries.GetLocalPlaylists;
using AutoMapper;
using Domain;
using Infrastructure.Database.Dtos;

namespace Infrastructure.Database.MappingProfiles
{
    using Domain.Entities;

    public class DatabaseMappingProfile : Profile
    {
        public DatabaseMappingProfile()
        {
            CreateMap<EntityDto, Entity>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Entity, EntityDto>();

            CreateMap<UserDto, UserProfile>()
                .IncludeBase<EntityDto, Entity>()
                .ForMember(dest => dest.SpotifyServiceProfile,
                   opt => opt.MapFrom(src => src.SpotifyServiceProfile))
                .ReverseMap();

            CreateMap<ProfileDto, ServiceProfile>()
                .ReverseMap();

            CreateMap<SpotifyTokensDto, Domain.Entities.Spotify.Tokens>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<PlaylistDto, Playlist>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<PlaylistDto, GetLocalPlaylists.PlaylistViewModel>();

            CreateMap<LastPlaylistCheckDto, LastPlaylistCheck>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();
        }
    }
}