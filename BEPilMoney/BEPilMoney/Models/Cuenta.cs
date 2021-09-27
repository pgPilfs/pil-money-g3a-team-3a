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
        public int TipoCuenta { get; set; }
        public int TipoDeMoneda { get; set; }
        public int Usuario { get; set; }
        public string CVU { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Alias { get; set; }
        public double Saldo { get; set; }
        #endregion

        public Cuenta(string alias) 
        {
            this.TipoCuenta = 1;
            this.TipoDeMoneda = 1;
            this.Usuario = 0;
            this.CVU = this.GenerarCVU();
            this.FechaAlta = DateTime.Now;
            this.Alias = alias;
            this.Saldo = 0.00;
        }

        private string GenerarCVU()
        {
            var characters = "0123456789";
            var Charsarr = new char[22];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);

            return resultString;
        }
    }
}