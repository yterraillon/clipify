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
    public class AccountController : ApiController
    {
        // GET api/<AccountController>
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            return await Mediator.Send(new GetUser.Request());
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task Post([FromBody] string accessToken, string refreshToken, int expiresIn)
        {
            await Mediator.Send(new CreateLocalUser.Command
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn
            });
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
