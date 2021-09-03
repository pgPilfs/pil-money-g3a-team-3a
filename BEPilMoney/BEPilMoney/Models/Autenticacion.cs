using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEPilMoney.Models
{
    public class Autenticacion
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Token { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        #endregion
    }
}