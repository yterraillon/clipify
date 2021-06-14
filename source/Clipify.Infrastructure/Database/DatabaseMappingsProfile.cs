using AutoMapper;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;
using LiteDB;

namespace Clipify.Infrastructure.Database
{
    public class DatabaseMappingsProfile : Profile
    {
        public DatabaseMappingsProfile()
        {
            CreateMap<EntityDto, Entity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()));

            CreateMap<Entity, EntityDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => new ObjectId(s.Id)));

            CreateMap<UserDto, User>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<PlaylistDto, Playlist>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();

            CreateMap<ForkedPlaylistDto, ForkedPlaylist>()
                .IncludeBase<EntityDto, Entity>()
                .ReverseMap();
        }
    }
}