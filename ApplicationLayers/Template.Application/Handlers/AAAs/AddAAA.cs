using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Application.Logic;
using Template.Application.Mediator.Notifications.Events.Usuarios;
using Template.Application.Mediator.Responses;
using Template.Application.Models.Account;
using Template.Domain.AppSettings;
using Template.Domain.Exceptions;

namespace Template.Application.Handlers.AAAs
{
    public class AddAAA : IRequestWrapper<int>
    {
        public AddAAA(string nombre, string apellido, string cedula)
        {
            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }

    }

    ////////////////// VALIDATOR ///////////////////

    public class AddAAAValidator : AbstractValidator<AddAAA>
    {
        public AddAAAValidator()
        {
            //this.CascadeMode = CascadeMode.Stop; // si falla primera validacion no sigue con las otras
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Cedula).NotEmpty();
            // Si no cumple el When entonces no valida las rules que tiene adentro
            When(x => !string.IsNullOrEmpty(x.Cedula), () =>
            {
                RuleFor(x => x.Cedula).NotEmpty().Must(c => c != null && c.Replace(".", "").Replace("-", "").Length == 8)
                    .WithMessage("'Cedula' tiene que ser de 8 dígitos.");
            });
            //Transform(x => x.Edad, StringToNullableInt).GreaterThan(18);
        }

        //int? StringToNullableInt(string value) => int.TryParse(value, out int val) ? (int?)val : null;
    }

    ////////////////// HANDLER /////////////////////

    public class AddAAAHandler : IHandlerWrapper<AddAAA, int>
    {
        private readonly AppUser CurrentUser;
        private readonly ConfigurationSettings _config;
        private readonly IUnitLogic _unitLogic;
        private readonly IMediator? _mediator;

        public AddAAAHandler(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationSettings> config, IUnitLogic unitLogic, IMediator? mediator)
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

        public async Task<Response<int>> Handle(AddAAA request, CancellationToken cancellationToken)
        {
            try
            {
                if (CurrentUser.TieneRol(UserRol.Empleado) || CurrentUser.TieneRol(UserRol.Developer))
                {
                    throw new UnAuthorizedException("No está autorizado a agregar AAAs.");
                }

                int result = await _unitLogic.AAALogic.Add(request);

                await Publish(new MessageEvent("Se ha registrado un empleado nuevo."));

                return await Task.FromResult(Response.Ok(result, "AAA agregado a la base de datos."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Response.Fail<int>(ex));
            }
        }
    }
}
