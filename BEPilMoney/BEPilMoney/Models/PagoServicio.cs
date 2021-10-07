using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEPilMoney.Models
{
    public class PagoServicio
    {
        public int Id { get; set; }
        public int Servicio { get; set; }
        public int CuentaOrigen { get; set; }
        public string CVUServicio { get; set; }
        public DateTime FechaPago { get; set; }
        public double Monto { get; set; }

    }
}