using BEPilMoney.Models;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class LoginController : BaseController
    {
        [HttpPost]
        [ActionName("Login")]
        public IHttpActionResult Login([FromBody] Login log)
        {
            DataTable resp = new DataTable();

            if (log.Usuario != string.Empty && log.Password != string.Empty)
                resp = this.InicioSesion(log.Usuario, log.Password);
            else return BadRequest();
            return Ok(resp);
        }
    }
}
