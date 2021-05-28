﻿using Clipify.Application.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Domain.Entities;

namespace Clipify.Application.Users.Commands
{
    public static class CreateUser
    {
        public class Command : IRequest<Unit>
        {
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;

            public int ExpiresIn { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IRepository<User, string> _userRepository;

            public Handler(IRepository<User, string> userRepository)
            {
                _userRepository = userRepository;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _userRepository.Add(new User
                {
                    AccessToken = request.AccessToken,
                    RefreshToken = request.RefreshToken,
                    ExpiresIn = request.ExpiresIn
                });

                return Task.FromResult(Unit.Value);
            }
        }
    }
}