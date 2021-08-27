using System.Threading.Tasks;
using Application.SpotifyAuthentication.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        private readonly IMediator _mediator;

        public CallbackController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string code, [FromQuery] string state)
        {
            if (string.IsNullOrEmpty(code)) return BadRequest();

            var response = await _mediator.Send(new Login.Request(code, state));

            return response.IsSuccess
                ? Redirect("/")
                : (IActionResult)BadRequest();
        }
    }
}
