using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Models.Usuarios
{
    public class UsuarioModel
    {
        public string? UserName { get; set; }

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool Habilitado { get; set; } = true;
    }
}
