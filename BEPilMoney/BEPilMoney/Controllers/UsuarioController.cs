using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using BEPilMoney.Models;
using BEPilMoney.Repositorios;


namespace BEPilMoney.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class UsuarioController : BaseController
    {
        private UsuarioRepositorio _usuario = new UsuarioRepositorio();

        [HttpGet]
        [Route("api/ListadoUsuario")]
        public IHttpActionResult GetUsuarios()
        {
            var listadoUsuario = this._usuario.Listado();
            if (listadoUsuario == null || listadoUsuario.Rows.Count == 0) return NotFound();
            return Ok(listadoUsuario);
        }

        [HttpGet]
        [Route("api/DetalleUsuario/{id:int}")]
        public IHttpActionResult GetUsuario(int id)
        {
            id = (id == 0) ? 0 : id;
            var DetalleDelUsuario = this._usuario.Detalle(id);
            if (DetalleDelUsuario == null || DetalleDelUsuario.Rows.Count == 0) return NotFound();
            return Ok(DetalleDelUsuario);
        }

        [HttpPost]
        [Route("api/Registrar")]
        public IHttpActionResult PostUsuario([FromBody] Usuario usuario)
        {
            var resp = this._usuario.Agregar(usuario);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/Modificar")]
        public IHttpActionResult PutUsuario([FromBody] Usuario usuario)
        {
            string token = usuario.autenticacion.Token;
            int estado = usuario.autenticacion.Estado;
            if(this.ValidarToken(token, estado) == false)
                return BadRequest();
            var resp = this._usuario.Modificar(usuario);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/Eliminar")]
        public IHttpActionResult DeleteUsuario([FromBody] Usuario usuario)
        {
            string token = usuario.autenticacion.Token;
            int estado = usuario.autenticacion.Estado;
            if (this.ValidarToken(token, estado) == false)
                return BadRequest();
            var resp = this._usuario.Eliminar(usuario.Id);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }
    }
}
