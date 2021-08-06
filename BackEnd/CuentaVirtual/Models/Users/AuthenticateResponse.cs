using System;
namespace CuentaVirtual.Models.Users
{
    /* El modelo de respuesta define los datos devueltos por el method de
     * autenticacion del controlador de usuarios. Incluye detalles basicos 
     * del usuario y un token JWT.
     */
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        public AuthenticateResponse()
        {
        }
    }
}
