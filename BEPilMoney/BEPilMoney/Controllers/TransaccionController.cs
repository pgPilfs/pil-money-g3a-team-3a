using BEPilMoney.Models;
using BEPilMoney.Repositorios;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BEPilMoney.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TransaccionController : ApiController
    {
        private TransaccionRepositorio _trans = new TransaccionRepositorio();

        [HttpPost]
        [Route("api/IngresoDinero")]
        public IHttpActionResult IngresarDinero([FromBody] Transaccion tran)
        {
            var resp = this._trans.Agregar(tran);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/ListadoTransacciones")]
        public IHttpActionResult UltimosMovimientos()
        {
            DataTable datos = this._trans.Listado();
            return Ok(datos);
        }
    }
}
