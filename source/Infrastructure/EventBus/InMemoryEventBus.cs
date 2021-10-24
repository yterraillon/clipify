using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Infrastructure.EventBus
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public InMemoryEventBus(IMediator mediator) => _mediator = mediator;

        public Task Publish<T>(T @event) where T : INotification
        {
            _mediator.Publish(@event);
            return Task.CompletedTask;
        }
    }
}