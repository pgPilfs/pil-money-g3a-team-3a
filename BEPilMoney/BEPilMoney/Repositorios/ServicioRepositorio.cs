using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public int PagoServicio(PagoServicio obj)
        {
            string spName = "PilMoney_Api_PagoServicio";
            obj.FechaPago = DateTime.Now;
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Servicio", obj.Servicio),
                new SqlParameter("@CuentaOrigen",obj.CuentaOrigen),
                new SqlParameter("@CVUServicio", obj.CVUServicio),
                new SqlParameter("@FechaPago",obj.FechaPago),
                new SqlParameter("@Monto",obj.Monto)
            };
            DAO dao = new DAO();
            int filaAfectada = dao.ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }

        public DataTable Detalle(int id)
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_DatosServicios";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@id", id)
            };
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName, listParam);
            return listado;
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