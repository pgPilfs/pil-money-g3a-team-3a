using System;
using System.ComponentModel.DataAnnotations;

namespace CuentaVirtual.Models.Users
{
    /* El modelo de solicitud de registro define los parametros para las 
     * solicitudes POST entrantes a la ruta /users/register , se adjunta a la
     * ruta configurandola como parametro del method de registro del controlador
     * de usuarios. 
     * Cuando la ruta recibe una solicitud HTTP POST, los datos 
     * se vinculan a una instancia de la clase RegisterRequest, se validan y se
     * pasan al method.
     */
    public class RegisterRequest
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Dni { get; set; }
        
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public RegisterRequest()
        {
        }
    }
}
