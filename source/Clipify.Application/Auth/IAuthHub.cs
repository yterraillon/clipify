using System.Threading.Tasks;

namespace Clipify.Application.Auth
{
    public interface IAuthHub
    {
        Task Broadcast(string message);
    }
}