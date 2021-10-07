using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BEPilMoney.Models;
using BEPilMoney.Repositorios;

namespace BEPilMoney.Controllers

{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServicioController : BaseController
    {
        private ServicioRepositorio _servicio = new ServicioRepositorio();

        [HttpGet]
        [Route("api/ListadoDeServicios")]
        public IHttpActionResult GetTipoServicio()
        {
            DataTable listadoTipoServicio = _servicio.ListadoTipoServicio();
            if (listadoTipoServicio != null) this.BadRequest();
            return this.Ok(listadoTipoServicio);
        }

        [HttpPost]
        [Route("api/PagoServicio")]
        public IHttpActionResult PagoServicio([FromBody] PagoServicio pago)
        {
            var resp = this._servicio.PagoServicio(pago);
            if (resp == 0) return BadRequest();
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/DatosServicios")]
        public IHttpActionResult GetTipoServicio([FromBody] String[] ServicioId)
        {

            int id = Convert.ToInt32(ServicioId[0].ToString());
            if (id == 0) return BadRequest();
            DataTable DatosServicios = _servicio.Detalle(id);
            if (DatosServicios != null) this.BadRequest();
            return this.Ok(DatosServicios);
        }

    }
}
