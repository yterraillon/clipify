using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;

namespace Clipify.Application.Playlists.Commands.SavePlaylist
{
    public static class SavePlaylist
    {
        public class Command : IRequest
        {
            public string PlaylistId { get; set; } = string.Empty;

            public string SnapshotId { get; set; } = string.Empty;

            public string Title { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            private readonly ICurrentUserService _currentUserService;

            public Handler(IRepository<Playlist, string> playlistRepository, ICurrentUserService currentUserService)
            {
                _playlistRepository = playlistRepository;
                _currentUserService = currentUserService;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!_currentUserService.IsUserLoggedIn())
                    return Unit.Task;

                var user = _currentUserService.GetCurrentUser();

                try
                {
                    _playlistRepository.Add(new Playlist
                    {
                        Created = DateTime.Now,
                        CreatedBy = user.Id,
                        PlaylistId = request.PlaylistId,
                        LastCheckedDate = DateTime.UtcNow,
                        LastModifiedDate = DateTime.UtcNow,
                        SnapshotId = request.SnapshotId,
                        Title = request.Title,
                        Updated = DateTime.UtcNow,
                        UpdatedBy = user.Id
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }


                return Unit.Task;
            }
        }
    }
}