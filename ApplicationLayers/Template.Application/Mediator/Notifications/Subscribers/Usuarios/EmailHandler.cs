using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Template.Application.Mediator.Notifications.Events.Usuarios;

namespace Template.Application.Mediator.Notifications.Subscribers.Usuarios
{
    public class EmailHandler : INotificationHandler<RegisterEvent>
    {

        public Task Handle(RegisterEvent notification, CancellationToken cancellationToken)
        {
            var message = "Email: " + notification.Message;

            // Enviar Email

            return Task.FromResult(0);
        }
    }
}
