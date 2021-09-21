using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BEPilMoney.AccesoADatos;
using BEPilMoney.Models;

namespace BEPilMoney.Repositorios
{
    public class ServicioRepositorio : IRepositorio<TipoServicio>
    {

        public int Agregar(TipoServicio obj)
        {
            throw new NotImplementedException();
        }

        public DataTable Detalle(int id)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public DataTable Listado()
        {
            throw new NotImplementedException();
        }

        public int Modificar(TipoServicio obj)
        {
            throw new NotImplementedException();
        }

        public DataTable ListadoTipoServicio()
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_ListadoDeServicios";
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName);
            return listado;
        }
    }
}