using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Handlers.Account;

namespace Example.Template.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IMediator mediator) : base(mediator) { }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuario request)
        {
            return Ok(await Mediator.Send(request));
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUsuario request)
        {
            return Ok(await Mediator.Send(request));
        }

        [Authorize]
        [HttpGet("ActualizarClaims")]
        public async Task<IActionResult> ActualizarClaims()
        {
            // Si en la app se guardaron preferencias o roles nuevos, entonces generamos nuevo token y se cargan los nuevos claims.
            return Ok(await Mediator.Send(new ActualizarClaims()));
        }
    }
}
