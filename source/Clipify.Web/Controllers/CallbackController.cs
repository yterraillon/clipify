using Clipify.Application.Auth.Requests.TokenRequest;
using Clipify.Application.Database;
using Clipify.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clipify.Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        private readonly IMediator _mediator;

        public CallbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string code, [FromQuery] string state)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest();

            var response = await _mediator.Send(new GetAccessToken.Request
            {
                Code = code,
            });

            if (string.IsNullOrEmpty(response.AccessToken))
                return BadRequest();

            await _mediator.Send(new CreateUser.Command
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpiresIn = response.ExpiresIn
            });

            return Redirect("/");
        }
    }
}
