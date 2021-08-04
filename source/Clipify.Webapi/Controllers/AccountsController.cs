using Clipify.Application.Users.Commands.CreateLocalUser;
using Clipify.Application.Users.Requests;
using Clipify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clipify.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ApiController
    {
        // GET api/<AccountController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetUser.Request());
            
            return Ok(response);
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string accessToken, string refreshToken, int expiresIn)
        {
            await Mediator.Send(new CreateLocalUser.Command
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn
            });

            return Ok();
        }
    }
}
