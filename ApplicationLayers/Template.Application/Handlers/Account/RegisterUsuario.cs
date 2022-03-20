using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Application.Logic;
using Template.Application.Mediator.Notifications.Events.Usuarios;
using Template.Application.Mediator.Responses;
using Template.Application.Models.Account;
using Template.Domain.AppSettings;

namespace Template.Application.Handlers.Account
{
    public class RegisterUsuario : IRequestWrapper<int>
    {
        public RegisterUsuario(string? userName, string email, string password, bool esAdmin)
        {
            UserName = userName;
            Email = email;
            Password = password;
            EsAdmin = esAdmin;
        }

        public string? UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool EsAdmin { get; set; }
    }

    ////////////////// VALIDATOR ///////////////////

    public class RegisterUsuarioCommandValidator : AbstractValidator<RegisterUsuario>
    {
        public RegisterUsuarioCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }

    ////////////////// HANDLER /////////////////////

    public class RegisterUsuarioHandler : IHandlerWrapper<RegisterUsuario, int>
    {
        private readonly AppUser CurrentUser;
        private readonly ConfigurationSettings _config;
        private readonly IUnitLogic _unitLogic;
        private readonly IMediator? _mediator;

        public RegisterUsuarioHandler(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationSettings> config, IUnitLogic unitLogic, IMediator? mediator)
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

        public async Task<Response<int>> Handle(RegisterUsuario request, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;

                bool userNameExiste = await _unitLogic.AccountLogic.ExisteUserName(request.UserName);
                if (userNameExiste) throw new Exception("El usuario ya existe.");
                bool emailExiste = await _unitLogic.AccountLogic.ExisteEmail(request.Email);
                if (emailExiste) throw new Exception("El email ya se encuentra en uso.");

                result = await _unitLogic.AccountLogic.RegistrarUsuario(request);

                // Agrega los Rols al usuario (que ya vengan en el request como lista Roles y un json de Preferencias)
                //
                //

                await Publish(new RegisterEvent("Se ha registrado un usuario nuevo."));

                return await Task.FromResult(Response.Ok(result, "Usuario registrado con éxito."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Response.Fail<int>(ex));
            }
        }
    }
}
