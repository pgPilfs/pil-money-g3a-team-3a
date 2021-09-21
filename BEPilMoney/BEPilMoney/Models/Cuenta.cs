using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEPilMoney.Models
{
    public class Cuenta
    {
        #region Propiedades
        public int Id { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public TipoDeMoneda TipoDeMoneda { get; set; }
        public Usuario Usuario { get; set; }
        public string CVU { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Alias { get; set; }
        public decimal Saldo { get; set; }
        #endregion
    }
}