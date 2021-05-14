using Clipify.Application;
using Clipify.Application.Auth.Requests.AccessTokenRequest;
using Clipify.Infrastructure.SpotifyAuth.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Clipify.Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        private readonly AuthHub _authHub;
        private readonly IHubContext<SpotifyAuthHub, ISignalRHub> _hubContext;
        private readonly IMediator _mediator;

        public CallbackController(IHubContext<SpotifyAuthHub, ISignalRHub> hubContext, IMediator mediator, AuthHub authHub)
        {
            _hubContext = hubContext;
            _mediator = mediator;
            _authHub = authHub;
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

            await _hubContext.Clients
                .Client(state)
                .Broadcast(response.AccessToken);

            return Redirect("/");
        }
    }
}
