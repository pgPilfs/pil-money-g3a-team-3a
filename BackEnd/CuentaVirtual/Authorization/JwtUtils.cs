using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CuentaVirtual.Entities;
using CuentaVirtual.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CuentaVirtual.Authorization
{
    /* La clase JWT utils contiene methods para generar y validar tokens JWT.
     * 
     * El method GenerateToken() genera un token JWT con la identificacion del
     * usuario especificado como el ID, lo que significa que la carga util
     * del token contendra la propiedad Id=<userId> (por ejemplo, "id":1).
     * 
     * El method ValidateToken() intenta validar el token JWT proporcionado
     * y devolver el Id de usuario ("id") de los reclamos del token. Si la
     * validacion falla, se devuelve un valor null.
     */
    //Interfaz
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(User user)
        {
            // Genera un token que es valido por 7 dias.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // establecer la desviación del reloj en cero para que los tokens
                    // caduquen exactamente a la hora de vencimiento del token
                    // (en lugar de 5 minutos después)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // Retorna la identificacion del usuario del token JWT
                // Si la validacion fue exitosa
                return userId;
            }
            catch
            {
                // Returna null si la validacion fallo
                return null;
            }
        }
    }
}
