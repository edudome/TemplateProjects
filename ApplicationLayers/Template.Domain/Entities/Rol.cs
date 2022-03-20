using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Core;

namespace Template.Domain.Entities
{
    public class Rol : TBase
    {
        public int RolId { get; set; }

        [NotMapped]
        public int PrimaryKey { get { return RolId; } set { RolId = value; } }

        public int? RolTypeId { get; set; }
        [ForeignKey("RolTypeId")]
        public RolType RolType { get; set; } = default!;

        public DateTime? Alta { get; set; } = default!;
    }
}
