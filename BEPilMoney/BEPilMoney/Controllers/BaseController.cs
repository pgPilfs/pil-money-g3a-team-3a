using BEPilMoney.Models;
using BEPilMoney.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BEPilMoney.Controllers
{
    public class BaseController : ApiController
    {
        private BaseRepositorio _br = new BaseRepositorio();

        public DataTable InicioSesion(string usuario, string password)
        {
            DataTable resp = new DataTable();

            if (usuario != string.Empty && password != string.Empty)
            {
                resp = this._br.Login(usuario, password);
            }

            return resp;
        }

        public bool ValidarToken(string token, int estado)
        {
            bool resp = false;
            if (token != string.Empty && estado == 1)
                resp = this._br.ValidarToken(token);
            return resp;
        }
    }
}
