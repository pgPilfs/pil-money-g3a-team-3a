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
    public class UsuarioController : ApiController
    {
        private UsuarioRepositorio _usuario = new UsuarioRepositorio();
        
        public IHttpActionResult Get()
        {
            var listadoUsuario = this._usuario.Listado();
            if (listadoUsuario == null || listadoUsuario.Rows.Count == 0) return NotFound();
            return Ok(listadoUsuario);
        }

        public IHttpActionResult Get(int id)
        {
            id = (id == 0) ? 0 : id;
            var DetalleDelUsuario = this._usuario.Detalle(id);
            if (DetalleDelUsuario == null || DetalleDelUsuario.Rows.Count == 0) return NotFound();
            return Ok(DetalleDelUsuario);
        }

        //public IHttpActionResult Post([FromBody] Usuario usuario)
        //{

        //    var resp = this._usuario.Agregar<Usuario>(usuario);
        //    if (resp == 0) return BadRequest();
        //    return Ok(resp);
        //}

        //public IHttpActionResult Put([FromBody] Usuario usuario)
        //{

        //    var resp = this._usuario.Modificar<Usuario>(usuario);
        //    if (resp == 0) return BadRequest();
        //    return Ok(resp);
        //}

        public IHttpActionResult Delete(int id)
        {
            id = (id == 0) ? 0 : id;
            var resp = this._usuario.Eliminar(id);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }
    }
}
