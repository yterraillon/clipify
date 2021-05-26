using AutoMapper;
using Clipify.Application.Database;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using Clipify.Infrastructure.Database.Dtos;

namespace Clipify.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public CurrentUserService(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User GetCurrentUser()
        {
            var user = _context.Database
                .GetCollection<UserDto>()
                .FindOne(x => !string.IsNullOrEmpty(x.AccessToken));

            return _mapper.Map<User>(user ?? UserDto.Empty);
        }
    }
}