using System.Linq;
using System.Threading.Tasks;
using Clipify.Application.Auth;
using Clipify.Application.Auth.Requests.AccessTokenRequest;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using Clipify.Infrastructure.SpotifyAuth.Hubs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Clipify.Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        private readonly IHubContext<SpotifyAuthHub, IAuthHub> _hubContext;
        private readonly IMediator _mediator;

        public CallbackController(IHubContext<SpotifyAuthHub, IAuthHub> hubContext, IMediator mediator)
        {
            _hubContext = hubContext;
            _mediator = mediator;
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

            await _hubContext.Clients.All.Broadcast(response.AccessToken);

            return Redirect("/");
        }
    }
}
