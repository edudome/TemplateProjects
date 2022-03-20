using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Core;

namespace Template.Domain.Entities
{
    public class Usuario : TBase
    {
        public int UsuarioId { get; set; }

        [NotMapped]
        public int PrimaryKey { get { return UsuarioId; } set { UsuarioId = value; } }

        // Se puede registrar o loguear con UserName o Email
        public string? UserName { get; set; }

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool Habilitado { get; set; } = true;
    }
}
