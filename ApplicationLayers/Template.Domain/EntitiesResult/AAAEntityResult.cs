using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.EntitiesResult
{
    public class AAAEntityResult
    {
        public string Nombre { get; set; } = default!;

        public string Apellido { get; set; } = default!;

        public string? Cedula { get; set; }

        public bool Habilitado { get; set; } = true;
    }
}
