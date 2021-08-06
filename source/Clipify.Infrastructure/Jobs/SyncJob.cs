using Clipify.Application.Playlists.Commands.SyncPlaylists;
using Quartz;
using System.Threading.Tasks;
using MediatR;

namespace Clipify.Infrastructure.Jobs
{
    public class SyncJob : IJob
    {
        private readonly IMediator _mediator;

        public SyncJob(IMediator mediator) => _mediator = mediator;

        /// <inheritdoc />
        public Task Execute(IJobExecutionContext context) => _mediator.Send(new SyncPlaylists.Command());
    }
}