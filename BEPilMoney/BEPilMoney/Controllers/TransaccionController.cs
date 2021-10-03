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

        [HttpPost]
        [Route("api/TransferirDinero")]
        public IHttpActionResult TransferirDinero([FromBody] Transaccion tran)
        {
            var resp = this._trans.Transferir(tran);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/ListadoTransacciones")]
        public IHttpActionResult UltimosMovimientos([FromBody] string[] id_user)
        {
            int id = Convert.ToInt32(id_user[0].ToString());
            if (id == 0) return BadRequest();
            DataTable datos = this._trans.Detalle(id);
            return Ok(datos);
        }
    }
}
