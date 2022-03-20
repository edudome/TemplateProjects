using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Template.Application.Mediator.Notifications.Events.Usuarios;

namespace Template.Application.Mediator.Notifications.Subscribers.Usuarios
{
    public class LoggingHandler : INotificationHandler<RegisterEvent>, INotificationHandler<MessageEvent>
    {

        public Task Handle(RegisterEvent notification, CancellationToken cancellationToken)
        {
            var message = "Logging: " + notification.Message;

            return Task.FromResult(0);
        }

        public Task Handle(MessageEvent notification, CancellationToken cancellationToken)
        {

            var message = "Mensaje: " + notification.Message;

            return Task.FromResult(0);
        }
    }
}
