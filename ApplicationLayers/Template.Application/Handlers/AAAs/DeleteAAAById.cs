using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Application.Logic;
using Template.Application.Mediator.Responses;
using Template.Application.Models.Account;
using Template.Application.Models.AAAs;
using Template.Domain.AppSettings;
using Template.Domain.Exceptions;
using FluentValidation;

namespace Template.Application.Handlers.AAAs
{
    public class DeleteAAAById : IRequestWrapper<bool>
    {
        public DeleteAAAById(int aaaId)
        {
            AAAId = aaaId;
        }

        public int AAAId { get; set; }
    }

    ////////////////// VALIDATOR ///////////////////

    public class DeleteAAAByIdValidator : AbstractValidator<DeleteAAAById>
    {
        public DeleteAAAByIdValidator()
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

    public class DeleteAAAByIdHandler : IHandlerWrapper<DeleteAAAById, bool>
    {
        private readonly AppUser CurrentUser;
        private readonly ConfigurationSettings _config;
        private readonly IUnitLogic _unitLogic;
        private readonly IMediator? _mediator;

        public DeleteAAAByIdHandler(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationSettings> config, IUnitLogic unitLogic, IMediator? mediator)
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

        public async Task<Response<bool>> Handle(DeleteAAAById request, CancellationToken cancellationToken)
        {
            try
            {
                AAAModel? result = await _unitLogic.AAALogic.GetById(request.AAAId);

                if (result == null)
                {
                    throw new NoDataException("El AAA no existe.");
                }

                await _unitLogic.AAALogic.DeleteById(request.AAAId);

                return await Task.FromResult(Response.Ok(true, "AAA eliminado."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Response.Fail<bool>(ex));
            }
        }
    }
}
