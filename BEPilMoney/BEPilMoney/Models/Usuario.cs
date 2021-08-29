using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEPilMoney.Models
{
    public class Usuario
    {
        #region Propiedades
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string FotoPerfil { get; set; }
        public string FotoDNI { get; set; }
        #endregion
    }
}