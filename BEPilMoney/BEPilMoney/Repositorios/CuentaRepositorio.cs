using BEPilMoney.AccesoADatos;
using BEPilMoney.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BEPilMoney.Repositorios
{
    public class CuentaRepositorio : IRepositorio<Cuenta>
    {
        public int Agregar(Cuenta obj)
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

        public DataTable DatosCuentaPesos(int id)
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_DatosCuentaPeso";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", id),
            };
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName, listParam);
            return listado;
        }

        public DataTable UltimosMovimientos(int id)
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_UltimosMovimiento";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", id),
            };
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName, listParam);
            return listado;
        }

        public int Modificar(Cuenta obj)
        {
            throw new NotImplementedException();
        }
    }
}