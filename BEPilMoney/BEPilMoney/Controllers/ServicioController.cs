﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BEPilMoney.Repositorios;

namespace BEPilMoney.Controllers

{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class ServicioController : BaseController
    {
        private ServicioRepositorio _servicio = new ServicioRepositorio();

        //localhost:port/api/nombreControlador/accion/parametroOpcional
        [HttpGet]
        [ActionName("Listado")]
        public IHttpActionResult GetTipoServicio()
        {
            DataTable listadoTipoServicio = _servicio.ListadoTipoServicio();

            if (listadoTipoServicio != null) this.BadRequest();
            return this.Ok(listadoTipoServicio);
        }
    }
}
