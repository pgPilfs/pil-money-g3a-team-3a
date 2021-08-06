using System;
using System.Text.Json.Serialization;

namespace CuentaVirtual.Entities
{
    /* La clase de entidad de user representa los datos almacenados en la base
     * de datos para el usuario.
     * 
     * Las clases de entidad tambien se usan para pasar datos entre diferentes
     * partes de la aplicacion (Por ejemplo entre servicios y controladores) y
     * se puede usar para devolver datos de respuesta HTTP de los methods del 
     * controlador
     * 
     * El atributo [JsonIgnore] evita que la propiedad PasswordHash 
     * se serialice y devuelva en las respuestas de la API.
     */
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public float CapitalPesos { get; set; }
        public float CapitalDolares { get; set; }
        public float CapitalCriptomonedas { get; set; }
        public byte[] ImgDoc1 { get; set; }
        public byte[] ImgDoc2 { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }


        public User()
        {
        }
    }
}
