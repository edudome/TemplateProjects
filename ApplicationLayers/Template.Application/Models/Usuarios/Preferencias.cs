using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Models.Usuarios
{
    public class Preferencias
    {
        public bool UsaTemaOscuro { get; set; } = default!;

        public int MaximoMensajes { get; set; } = default!;
    }
}
