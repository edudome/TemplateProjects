using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.InterfacesRepository;

namespace Template.Domain.Core
{
    public interface IUnitOfWork
    {
        ISqlDataAccess SqlDataAccess { get; }

        IUsuarioRepository Usuarios { get; }

        IAAARepository AAAs { get; }

    }
}
