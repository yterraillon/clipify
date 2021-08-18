using AutoMapper;
using Clipify.Domain.Entities;
using Infrastructure.Database.Dtos;
using LiteDB;

namespace Infrastructure.Database.MappingProfiles
{
    public class DatabaseMappingProfile : Profile
    {
        public DatabaseMappingProfile()
        {
            CreateMap<EntityDto, Entity>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Entity, EntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

            CreateMap<UserDto, User>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<SpotifyProfileDto, Domain.Spotify.Profile>()
                .ReverseMap();
            CreateMap<SpotifyTokensDto, Domain.Spotify.Tokens>()
                .ReverseMap();
        }
    }
}