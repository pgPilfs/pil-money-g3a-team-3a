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
    public class CuentaController : BaseController
    {
        private CuentaRepositorio _cuenta = new CuentaRepositorio();

        [HttpPost]
        [Route("api/CuentaPesos")]
        public IHttpActionResult DatosCuentaPesos([FromBody] string[] id_user)
        {
            int id = Convert.ToInt32(id_user[0].ToString());
            if (id == 0) return BadRequest();
            DataTable datos = this._cuenta.DatosCuentaPesos(id);
            return Ok(datos);
        }

        [HttpPost]
        [Route("api/UltimosMovimientos")]
        public IHttpActionResult UltimosMovimientos([FromBody] string[] id_user)
        {
            int id = Convert.ToInt32(id_user[0].ToString());
            if (id == 0) return BadRequest();
            DataTable datos = this._cuenta.UltimosMovimientos(id);
            return Ok(datos);
        }
    }
}
