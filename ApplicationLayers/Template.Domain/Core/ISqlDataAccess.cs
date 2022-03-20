using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Core
{
    public interface ISqlDataAccess
    {
        Task<T?> Execute<T>(string query, object? parameters = null, int commandTimeout = 30);
    }
}
