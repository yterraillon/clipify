using System.Threading.Tasks;
using MediatR;

namespace Application
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : INotification;
    }
}