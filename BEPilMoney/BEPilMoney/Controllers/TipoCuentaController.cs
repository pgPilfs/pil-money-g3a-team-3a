using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BEPilMoney.Models;
using BEPilMoney.Repositorios;

namespace BEPilMoney.Controllers
{
    public class TipoCuentaController : ApiController
    {
        private TipoCuentaRepositorio _tipoCuenta = new TipoCuentaRepositorio();

        [HttpPost]
        public IHttpActionResult Post([FromBody] TipoCuenta tipoCuenta)  
        {
            int filasAfectadas = this._tipoCuenta.Agregar(tipoCuenta);
            if (filasAfectadas == 0) return BadRequest();
            return Ok(filasAfectadas);
        }
    }
}
