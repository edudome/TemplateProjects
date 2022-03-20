using MediatR;
using Template.Application.Mediator.Responses;

namespace Template.Application.Handlers
{
    public class BaseCommand : IRequestWrapper<int> { }

    public class BaseHandler : IHandlerWrapper<BaseCommand, int>
    {
        public Task<Response<int>> Handle(BaseCommand request, CancellationToken cancellationToken)
        {
            // ESTE HANDLER SE USA PARA REFERENCIARLO EN EL DEPENCY CONTAINER Y OBTENER ESTE ASSEMBLY Y DEFINIR LAS INTERFACES DE LOS HANDLERS
            // NO BORRAR
            throw new NotImplementedException();
        }
    }

    public interface IRequestWrapper<T> : IRequest<Response<T>>
    { }
    public interface IHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>> where TIn : IRequestWrapper<TOut>
    { }
}
