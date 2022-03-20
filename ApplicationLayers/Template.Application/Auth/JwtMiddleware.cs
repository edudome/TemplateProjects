using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Domain.AppSettings;

namespace Template.Application.Auth
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConfigurationSettings _config;


        public JWTMiddleware(RequestDelegate next, IOptions<ConfigurationSettings> config)
        {
            _next = next;
            _config = config.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? id = null;

            if (token != null)
            {
                id = JwtUtils.ValidateToken(
                    token,
                    _config.JWT?.SecretKey ?? "",
                    _config.JWT?.ValidIssuer ?? "",
                    _config.JWT?.ValidAudience ?? "");
            }

            if (id == -1)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 401; //UnAuthorized
                await context.Response.WriteAsync("El token NO es válido.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
