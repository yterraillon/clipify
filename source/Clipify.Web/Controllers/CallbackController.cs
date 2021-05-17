using Clipify.Application;
using Clipify.Application.Auth.Requests.AccessTokenRequest;
using Clipify.Infrastructure.SpotifyAuth.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Clipify.Domain.Entities;

namespace Clipify.Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        private readonly IHubContext<SpotifyAuthHub, ISignalRHub> _hubContext;
        private readonly IMediator _mediator;
        private readonly IDbContext _context;

        public CallbackController(IHubContext<SpotifyAuthHub, ISignalRHub> hubContext, IMediator mediator, IDbContext context)
        {
            _hubContext = hubContext;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string code, [FromQuery] string state)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
                return BadRequest();

            var response = await _mediator.Send(new GetAccessToken.Request
            {
                Code = code,
            });

            if (string.IsNullOrEmpty(response.AccessToken))
                return BadRequest();

            var collection = _context.Database.GetCollection<User>();

            collection.Insert(new User()
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpiresIn = response.ExpiresIn
            });
            collection.EnsureIndex(x => x.Id, true);

            await _hubContext.Clients
                .Client(state)
                .Broadcast(response.AccessToken);

            return Redirect("/");
        }
    }
}
