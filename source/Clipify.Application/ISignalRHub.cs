using System.Threading.Tasks;

namespace Clipify.Application
{
    public interface ISignalRHub
    {
        Task Broadcast(string message);
    }
}