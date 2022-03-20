using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Application.Logic;
using Template.Application.Mediator.Responses;
using Template.Application.Models.Account;
using Template.Domain.AppSettings;

namespace Template.Application.Handlers.AAAs.Empty
{
    public class XCommand : IRequestWrapper<int>
    {
        public XCommand(string value)
        {
            Valor = value;
        }

        public string Valor { get; set; }

    }

    ////////////////// VALIDATOR ///////////////////

    public class XCommandValidator : AbstractValidator<XCommand>
    {
        public XCommandValidator()
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

    public class XCommandHandler : IHandlerWrapper<XCommand, int>
    {
        private readonly AppUser CurrentUser;
        private readonly ConfigurationSettings _config;
        private readonly IUnitLogic _unitLogic;
        private readonly IMediator? _mediator;

        public XCommandHandler(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationSettings> config, IUnitLogic unitLogic, IMediator? mediator)
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

        public async Task<Response<int>> Handle(XCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _unitLogic.EmptyLogic.Do(request);
                return await Task.FromResult(Response.Ok(result, "Sucessfully"));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Response.Fail<int>(ex));
            }
        }
    }
}
