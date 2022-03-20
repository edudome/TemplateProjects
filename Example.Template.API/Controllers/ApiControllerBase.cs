using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Template.Application.Models.Account;

namespace Example.Template.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        public readonly IMediator Mediator;

        public ApiControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(User);
            }
        }
    }

    ////// PUEDO CAMBIAR ESTE PARA REDIRECCIONAR AL LOGIN CUANDO SE ESTÉ LLAMANDO DESDE UNA APP MVC

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;

            // verificamos que exista el current user, que ya este logueado, y que tenga claims, si no tiene entonces devolvemos 401
            ClaimsPrincipal? user = context.HttpContext.User;
            if (user == null || user.FindFirst("Id")?.Value == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
