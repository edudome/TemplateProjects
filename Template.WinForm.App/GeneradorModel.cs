using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WinForm.App
{
    public class GeneradorModel
    {
        public string ControllerName { get; set; } = default!;
        public string ControllerMetodo { get; set; } = default!;
        public string ControllerVerbo { get; set; } = default!;
        public string CommandName { get; set; } = default!;
        public string CommandPropiedades { get; set; } = default!;
        public string LogicName { get; set; } = default!;
        public string LogicMetodo { get; set; } = default!;
        public string LogicParametros { get; set; } = default!;
        public string LogicResult { get; set; } = default!;
        public string Modelo { get; set; } = default!;
        public string EntityResult { get; set; } = default!;
        public string Entity { get; set; } = default!;

    }
}
