using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Template.Application.Mediator.Notifications.Events.Usuarios
{
    public class MessageEvent : INotification
    {
        public string Message;

        public MessageEvent(string message)
        {
            Message = message;
        }
    }
}
