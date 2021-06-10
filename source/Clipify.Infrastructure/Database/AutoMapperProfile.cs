using AutoMapper;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;
using LiteDB;

namespace Clipify.Infrastructure.Database
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()));

            CreateMap<User, UserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => new ObjectId(s.Id)));
        }
    }
}