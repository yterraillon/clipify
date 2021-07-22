using Clipify.Application.Playlists.Commands.SyncPlaylists;
using Clipify.Application.Users;
using Quartz;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.Jobs
{
    public class SyncJob : IJob
    {
        private readonly ISyncService _syncService;
        private readonly ICurrentUserService _currentUserService;

        public SyncJob(ISyncService syncService, ICurrentUserService currentUserService)
        {
            _syncService = syncService;
            _currentUserService = currentUserService;
        }

        /// <inheritdoc />
        public Task Execute(IJobExecutionContext context)
        {
            return _syncService.SyncAllPlaylistsAsync(_currentUserService.GetCurrentUser(), context.CancellationToken);
        }
    }
}