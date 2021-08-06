using System;
using System.ComponentModel.DataAnnotations;

namespace CuentaVirtual.Models.Users
{
    /* El modelo de solicitud de autenticacion define los parametros para las
     * solicitudes POST entrantes a la ruta /users/authenticate, se adjunta a 
     * la ruta configurandola como parametro del method de autenticacion del 
     * controlador de usuarios.
     * Cuando la ruta recibe una solicitud HTTP POST, los datos del cuerpo, se 
     * vinculan a una instancia de la clase AuthenticateRequest, se validan y
     * se pasan al method.
     * 
     * Las anotaciones de datos .NET se utilizan para manejar automaticamente
     * la validacion del modelo, el atributo [Required] establece tanto el 
     * nombre de usuario como la contraseña como campos obligatorios, por lo 
     * que si falta alguno de ellos, se devuelve un mensaje de error de 
     * validacion de la API.
     */
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public AuthenticateRequest()
        {
        }
    }
}
