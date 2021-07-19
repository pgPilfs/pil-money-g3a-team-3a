using System;
namespace CuentaVirtual.Models.Users
{
    /* El method de solicitud de actualizacion define los parametros para las 
     * solicitudes PUT entrantes a la ruta /users/{id}, se adjunta a la ruta 
     * configurandola como parametro del method de actualizacion del controlador
     * de usuarios.
     * Cuando la ruta recibe una solicitud HTTP PUT, los datos del cuerpo se 
     * vinculan a una instancia de la clase UpdateRequest 
     * 
     * Ninguna de las propiedades tiene el atributo [Required], por lo que todas
     * son opcionales y los campos omitidos no se actualizan en la base de datos.
     */
    public class UpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UpdateRequest()
        {
        }
    }
}
