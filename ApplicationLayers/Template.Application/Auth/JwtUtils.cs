using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Template.Application.Models.Account;
using Template.Application.Models.Usuarios;
using Template.Domain.AppSettings;

namespace Template.Application.Auth
{
    public static class JwtUtils
    {
        public static TokenInfo? GenerarToken(int? usuarioId, string? userName, string? email, List<string>? userRoles, Preferencias? preferencias, JWT? jwtConfig)
        {
            try
            {
                if (jwtConfig == null) throw new Exception("Falta información del Token en el appsettings.");

                var authClaims = new List<Claim>
                {
                    new Claim("Id", usuarioId?.ToString() ?? string.Empty),
                    new Claim("UserName", userName ?? string.Empty),
                    new Claim("Email", email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                if (userRoles != null)
                {
                    foreach (var rol in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, rol));
                    }
                }

                if (preferencias != null)
                {
                    foreach (var propertyInfo in preferencias.GetType().GetProperties())
                    {
                        authClaims.Add(new Claim(propertyInfo.Name, propertyInfo.GetValue(preferencias, null)?.ToString() ?? ""));
                    }
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
                var token = new JwtSecurityToken(
                issuer: jwtConfig.ValidIssuer,
                audience: jwtConfig.ValidAudience,
                expires: DateTime.Now.AddHours(jwtConfig.ExpiresInHours),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var result = new TokenInfo()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al GenerarToken: " + ex.Message);
            }
        }

        public static bool BorrarToken()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int? ValidateToken(string? token, string claveSecreta, string issuer, string audience)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                return userId;
            }
            catch
            {
                return -1;
            }
        }

        public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
