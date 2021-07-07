using AutoMapper;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;
using LiteDB;

namespace Clipify.Infrastructure.Database
{
    public class DatabaseProfile : Profile
    {
        public DatabaseProfile()
        {
            CreateMap<EntityDto, Entity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Entity, EntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

            CreateMap<UserDto, User>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<PlaylistDto, Playlist>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();
            
            CreateMap<TrackDto, Track>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();
        }
    }
}