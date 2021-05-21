using Clipify.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.TokenRequest;
using Clipify.Domain.Entities;

namespace Clipify.Web.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDbContext _context;

        public CallbackController(IMediator mediator, IDbContext context)
        {
            _mediator = mediator;
            _context = context;
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

            _context.Users.Insert(new User()
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpiresIn = response.ExpiresIn
            });

            return Redirect("/");
        }
    }
}
