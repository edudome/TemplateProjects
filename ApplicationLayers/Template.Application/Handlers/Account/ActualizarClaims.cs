using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Application.Auth;
using Template.Application.Logic;
using Template.Application.Mediator.Responses;
using Template.Application.Models.Account;
using Template.Application.Models.Usuarios;
using Template.Domain.AppSettings;

namespace Template.Application.Handlers.Account
{
    public class ActualizarClaims : IRequestWrapper<TokenInfo?>
    {
        public ActualizarClaims()
        {

        }
    }

    ////////////////// VALIDATOR ///////////////////

    public class ActualizarClaimsValidator : AbstractValidator<ActualizarClaims>
    {
        public ActualizarClaimsValidator()
        {
            //this.CascadeMode = CascadeMode.Stop; // si falla primera validacion no sigue con las otras

            //RuleFor(x => x.XXX).NotEmpty().MaximumLength(25);
            // Si no cumple el When entonces no valida las rules que tiene adentro
            //When(x => !string.IsNullOrEmpty(x.Cedula), () =>
            //{
            //    RuleFor(x => x.Cedula).NotEmpty().Must(c => c != null && c.Replace(".", "").Replace("-", "").Length == 8)
            //        .WithMessage("'Cedula' tiene que ser de 8 dígitos.");
            //});
            //Transform(x => x.Edad, StringToNullableInt).GreaterThan(18);
        }

        //int? StringToNullableInt(string value) => int.TryParse(value, out int val) ? (int?)val : null;
    }

    ////////////////// HANDLER /////////////////////

    public class ActualizarClaimsHandler : IHandlerWrapper<ActualizarClaims, TokenInfo?>
    {
        private readonly AppUser CurrentUser;
        private readonly ConfigurationSettings _config;
        private readonly IUnitLogic _unitLogic;
        private readonly IMediator? _mediator;

        public ActualizarClaimsHandler(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationSettings> config, IUnitLogic unitLogic, IMediator? mediator)
        {
            CurrentUser = (httpContextAccessor.HttpContext != null) ? new AppUser(httpContextAccessor.HttpContext.User) : new AppUser();
            _config = config.Value;
            _unitLogic = unitLogic;
            _mediator = mediator;
        }

        public async Task Publish(INotification evento)
        {
            if (_mediator != null) await _mediator.Publish(evento);
        }

        public async Task<Response<TokenInfo?>> Handle(ActualizarClaims request, CancellationToken cancellationToken)
        {
            try
            {
                TokenInfo? result = null;

                UsuarioModel? usuario = await _unitLogic.AccountLogic.GetUsuarioById(CurrentUser.Id);
                if (usuario != null)
                {
                    result = await _unitLogic.AccountLogic.LoginUsuario(usuario.UserName ?? usuario.Email, usuario.Password, _config.JWT);
                    JwtUtils.BorrarToken(); // borra token anterior (puede cambiarle la expiración capáz)
                }
                else
                {
                    throw new Exception("No se encuentra logueado.");
                }

                return await Task.FromResult(Response.Ok(result, "Nuevo token obtenido."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Response.Fail<TokenInfo?>(ex));
            }
        }
    }
}
