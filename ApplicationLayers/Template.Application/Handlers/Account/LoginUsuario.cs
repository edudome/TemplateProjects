using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Application.Logic;
using Template.Application.Mediator.Responses;
using Template.Application.Models.Account;
using Template.Domain.AppSettings;
using Template.Domain.Exceptions;

namespace Template.Application.Handlers.Account
{
    public class LoginUsuario : IRequestWrapper<TokenInfo?>
    {
        public LoginUsuario(string usuario, string password)
        {
            Usuario = usuario;
            Password = password;
        }

        public string Usuario { get; set; }

        public string Password { get; set; }
    }

    ////////////////// VALIDATOR ///////////////////

    public class LoginUsuarioValidator : AbstractValidator<LoginUsuario>
    {
        public LoginUsuarioValidator()
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

    public class LoginUsuarioHandler : IHandlerWrapper<LoginUsuario, TokenInfo?>
    {
        private readonly AppUser CurrentUser;
        private readonly ConfigurationSettings _config;
        private readonly IUnitLogic _unitLogic;
        private readonly IMediator? _mediator;

        public LoginUsuarioHandler(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationSettings> config, IUnitLogic unitLogic, IMediator? mediator)
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

        public async Task<Response<TokenInfo?>> Handle(LoginUsuario request, CancellationToken cancellationToken)
        {
            try
            {
                TokenInfo? result = await _unitLogic.AccountLogic.LoginUsuario(request.Usuario, request.Password, _config.JWT);

                return await Task.FromResult(Response.Ok(result, "Token info obtenida."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Response.Fail<TokenInfo?>(ex));
            }
        }
    }
}
