using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BEPilMoney.Repositorios;


namespace BEPilMoney.Controllers
{
    public class UsuarioController : ApiController
    {
        private UsuarioRepositorio _usuario = new UsuarioRepositorio();
        
        public IHttpActionResult Get()
        {
            var listadoUsuario = this._usuario.ListadoDeUsuario();
            if (listadoUsuario == null || listadoUsuario.Rows.Count == 0) return NotFound();
            return Ok(listadoUsuario);
        }
    }
}
