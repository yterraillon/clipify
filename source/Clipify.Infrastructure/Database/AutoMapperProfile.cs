using AutoMapper;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;

namespace Clipify.Infrastructure.Database
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}