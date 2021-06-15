using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common;
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

        public class Handler : BaseHandler, IRequestHandler<Command>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;


            public Handler(IRepository<Playlist, string> playlistRepository, ICurrentUserService currentUserService) : base(currentUserService)
            {
                _playlistRepository = playlistRepository;
            }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    _playlistRepository.Add(new Playlist
                    {
                        Created = DateTime.Now,
                        CreatedBy = CurrentUser.Id,
                        PlaylistId = request.PlaylistId,
                        LastCheckedDate = DateTime.UtcNow,
                        LastModifiedDate = DateTime.UtcNow,
                        SnapshotId = request.SnapshotId,
                        Title = request.Title,
                        Updated = DateTime.UtcNow,
                        UpdatedBy = CurrentUser.Id
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