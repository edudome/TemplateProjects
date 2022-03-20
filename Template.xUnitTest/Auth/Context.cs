using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Template.Application.Handlers.Account;
using Template.Application.Logic;
using Template.Application.Models.Usuarios;
using Template.Domain.AppSettings;

namespace Template.xUnitTest.Auth
{
    public class Context
    {
        public Context(IOptions<ConfigurationSettings> configuration, IUnitLogic unitLogic)
        {
            _configuration = configuration;
            _unitLogic = unitLogic;
        }

        public IOptions<ConfigurationSettings> _configuration { get; set; }

        public IUnitLogic _unitLogic { get; }


        public async Task<IHttpContextAccessor> GetCurrentUser(List<string>? roles = null, Preferencias? preferencias = null)
        {
            //Mock IHttpContextAccessor
            Mock<IHttpContextAccessor> mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            int usuarioId = 1;
            string userName = "unitTest";
            string email = "unit@testing.com";
            string password = "unit123";
            List<string> rolesValues = new List<string>() { "Administrador" };
            Preferencias preferenciasValues = new Preferencias() { UsaTemaOscuro = true, MaximoMensajes = 77 };

            if (roles == null) roles = rolesValues;
            if (preferencias == null) preferencias = preferenciasValues;

            List<Claim> authClaims = new List<Claim>
            {
                new Claim("Id", usuarioId.ToString()),
                new Claim("UserName", userName),
                new Claim("Email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var rol in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, rol));
            }

            foreach (var propertyInfo in preferencias.GetType().GetProperties())
            {
                authClaims.Add(new Claim(propertyInfo.Name, propertyInfo.GetValue(preferencias, null)?.ToString() ?? ""));
            }

            ClaimsIdentity identity = new ClaimsIdentity(authClaims);
            DefaultHttpContext context = new DefaultHttpContext()
            {
                User = new System.Security.Claims.ClaimsPrincipal(identity)
            };
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            IHttpContextAccessor httpContextAccessor = mockHttpContextAccessor.Object;

            ////

            var request = new RegisterUsuario(
                userName,
                email,
                password,
                roles.Any(r => r.Equals("Administrador"))
            );

            var registerUsuarioHandler = new RegisterUsuarioHandler(
                httpContextAccessor,
                _configuration,
                _unitLogic,
                null
            );

            var cancellationToken = new CancellationToken();
            var response = await registerUsuarioHandler.Handle(request, cancellationToken);

            ////

            return httpContextAccessor;
        }
    }
}
