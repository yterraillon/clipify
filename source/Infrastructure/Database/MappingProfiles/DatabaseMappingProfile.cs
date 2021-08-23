using AutoMapper;
using Infrastructure.Database.Dtos;
using LiteDB;

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
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

            CreateMap<UserDto, UserProfile>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<SpotifyProfileDto, Domain.Entities.Spotify.Profile>()
                .ReverseMap();

            CreateMap<SpotifyTokensDto, Domain.Entities.Spotify.Tokens>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();
        }
    }
}