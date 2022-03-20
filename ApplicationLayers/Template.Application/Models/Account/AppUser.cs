using System.Security.Claims;
using Template.Domain.Core;

namespace Template.Application.Models.Account
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal) : base(principal)
        {
        }

        public AppUser() { }

        public int Id { get { var valor = FindFirst("Id")?.Value; return Convert.ToInt32(string.IsNullOrEmpty(valor) ? 0 : valor); } }

        public string UserName { get { return FindFirst("UserName")?.Value ?? string.Empty; } }

        public string Email { get { return FindFirst("Email")?.Value ?? string.Empty; } }

        // Preferencias (crear clase Preferencias así se hace CurrentUser.Preferencias.UsaTemaOscuro)

        public bool UsaTemaOscuro { get { var valor = FindFirst("UsaTemaOscuro")?.Value; return Convert.ToBoolean(string.IsNullOrEmpty(valor) ? false : valor); } }

        public int MaximoMensajes { get { var valor = FindFirst("MaximoMensajes")?.Value; return Convert.ToInt32(string.IsNullOrEmpty(valor) ? 0 : valor); } }

        //

        public bool TieneRol(UserRol rol)
        {
            return IsInRole(rol.ToString());
        }
    }

    public enum UserRol
    {
        Administrador,
        Empleado,
        Developer
    }
}
