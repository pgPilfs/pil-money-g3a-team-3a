using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEPilMoney.Models
{
    public class Transaccion
    {
        #region Propiedades
        public int Id { get; set; }
        public int TipoTrans { get; set; }
        public int CuentaOrigen { get; set; }
        public string CuentaDestino { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        #endregion
    }
}