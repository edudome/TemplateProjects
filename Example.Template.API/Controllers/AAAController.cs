using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Handlers.AAAs;
using Template.Application.Mediator.Responses;
using Template.Application.Models.AAAs;

namespace Example.Template.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class AAAController : ApiControllerBase
    {
        public AAAController(IMediator mediator) : base(mediator) { }


        [HttpPost("AddAAA")]
        public async Task<Response<int>> AddAAA([FromBody] AddAAA request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("GetAAAById")]
        public async Task<Response<AAAModel?>> GetAAAById(int AAAId)
        {
            return await Mediator.Send(new GetAAAById(AAAId));
        }

        [HttpGet("GetAAAsAll")]
        public async Task<Response<List<AAAModel>?>> GetAAAsAll()
        {
            return await Mediator.Send(new GetAAAsAll());
        }

        [HttpPost("UpdateAAA")]
        public async Task<Response<bool>> UpdateAAA([FromBody] UpdateAAA request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("DeleteAAAById")]
        public async Task<Response<bool>> DeleteAAAById(int id)
        {
            return await Mediator.Send(new DeleteAAAById(id));
        }
    }
}
