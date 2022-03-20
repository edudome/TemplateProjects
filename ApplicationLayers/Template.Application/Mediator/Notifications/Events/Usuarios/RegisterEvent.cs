using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Template.Application.Mediator.Notifications.Events.Usuarios
{
    public class RegisterEvent : INotification
    {
        public string Message;

        public RegisterEvent(string message)
        {
            Message = message;
        }
    }
}
