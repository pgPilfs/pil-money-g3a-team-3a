using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using BEPilMoney.Models;
using BEPilMoney.Repositorios;


namespace BEPilMoney.Controllers
{
    public class UsuarioController : BaseController
    {
        private UsuarioRepositorio _usuario = new UsuarioRepositorio();

        [HttpPost]
        [ActionName("Login")]
        public IHttpActionResult Login([FromBody] Usuario usuario)
        {
            string resp = string.Empty;

            if (usuario.NombreUsuario != string.Empty && usuario.Clave != string.Empty) 
                resp = this.InicioSesion(usuario.NombreUsuario, usuario.Clave);
            else return BadRequest();
            return Ok(resp);
        }

        [HttpGet]
        [ActionName("Listado")]
        public IHttpActionResult GetUsuarios()
        {
            var listadoUsuario = this._usuario.Listado();
            if (listadoUsuario == null || listadoUsuario.Rows.Count == 0) return NotFound();
            return Ok(listadoUsuario);
        }

        [HttpGet]
        [ActionName("Detalle")]
        public IHttpActionResult GetUsuario(int id)
        {
            id = (id == 0) ? 0 : id;
            var DetalleDelUsuario = this._usuario.Detalle(id);
            if (DetalleDelUsuario == null || DetalleDelUsuario.Rows.Count == 0) return NotFound();
            return Ok(DetalleDelUsuario);
        }

        [HttpPost]
        [ActionName("Registrar")]
        public IHttpActionResult PostUsuario([FromBody] Usuario usuario)
        {
            string token = usuario.autenticacion.Token;
            int estado = usuario.autenticacion.Estado;
            if (this.ValidarToken(token, estado) == false)
                return BadRequest();
            var resp = this._usuario.Agregar(usuario);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }

        [HttpPost]
        [ActionName("Modificar")]
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

        [HttpDelete]
        [ActionName("Eliminar")]
        public IHttpActionResult DeleteUsuario(int id)
        {
            id = (id == 0) ? 0 : id;
            var resp = this._usuario.Eliminar(id);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }
    }
}
