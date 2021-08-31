using System.Threading.Tasks;
using MediatR;

namespace Application.Common.Interfaces
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : INotification;
    }
}