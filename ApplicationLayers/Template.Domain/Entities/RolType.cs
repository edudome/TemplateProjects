using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Core;

namespace Template.Domain.Entities
{
    public class RolType : TBase
    {
        public int RolTypeId { get; set; }

        [NotMapped]
        public int PrimaryKey { get { return RolTypeId; } set { RolTypeId = value; } }

        public string Nombre { get; set; } = default!;

        public string Descripcion { get; set; } = "Sin descripción";
    }
}
