using BEPilMoney.Models;
using BEPilMoney.Security;

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
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : BaseController
    {
        [HttpPost]
        [Route("api/Login")]
        public IHttpActionResult Login([FromBody] Login log)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            DataTable resp = new DataTable();

            if (log.Usuario != string.Empty && log.Password != string.Empty)
                resp = this.InicioSesion(log.Usuario, log.Password);
            if(resp.Rows.Count > 0)
            {
                var userName = resp.Rows[0]["NombreApellido"].ToString();
                var id_usuario = resp.Rows[0]["Id"].ToString();
                var token = TokenGenerator.GenerateTokenJwt(userName);
                string[] datosUsuario = { userName, id_usuario, token};
                return Ok(datosUsuario);
            } 
            else
            {
                return Ok(false);
            }
        }
    }
}
