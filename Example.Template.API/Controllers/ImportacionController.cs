using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Handlers.Importacion.AAAs;
using Template.Application.Mediator.Notifications.Events.Usuarios;
using Template.Application.Mediator.Responses;

namespace Example.Template.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ImportacionController : ApiControllerBase
    {
        public ImportacionController(IMediator mediator) : base(mediator) { }


        [HttpPost("ImportarAAAs")]
        public async Task<Response<bool>> ImportarAAAs([FromBody] ImportacionAAA request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("EnviarMensaje")]
        public async Task EnviarMensaje()
        {
            await Mediator.Publish(new MessageEvent("Mensaje de prueba."));
        }
    }
}
