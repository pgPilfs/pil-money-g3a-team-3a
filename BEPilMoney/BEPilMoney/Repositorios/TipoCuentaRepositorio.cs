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
	public class TipoCuentaRepositorio : IRepositorio<TipoCuenta>
	{
        public int Agregar(TipoCuenta obj)
        {
            string spName = "PilMoney_Api_AgregarTipoCuenta";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Tipo", obj.Tipo),
            };
            int filaAfectada = HelperSqlServer.GetHelperSqlServer().ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
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

        public int Modificar(TipoCuenta obj)
        {
            throw new NotImplementedException();
        }
    }
}