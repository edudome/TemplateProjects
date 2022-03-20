using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Core;

namespace Template.Domain.Entities
{
    public class AAA : TBase
    {
        public int AAAId { get; set; }

        [NotMapped]
        public int PrimaryKey { get { return AAAId; } set { AAAId = value; } }

        public string Nombre { get; set; } = default!;

        public string Apellido { get; set; } = default!;

        public string? Cedula { get; set; }

        public bool Habilitado { get; set; } = true;

    }
}
