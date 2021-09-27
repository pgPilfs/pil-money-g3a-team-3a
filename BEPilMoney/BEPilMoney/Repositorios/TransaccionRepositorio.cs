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
    public class TransaccionRepositorio : IRepositorio<Transaccion>
    {
        public int Agregar(Transaccion obj)
        {
            string spName = "PilMoney_Api_IngresarDinero";
            obj.Fecha = DateTime.Now;
            obj.TipoTrans = 2;
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@TipoTrans", obj.TipoTrans),
                new SqlParameter("@CuentaOrigen",obj.CuentaOrigen),
                new SqlParameter("@CuentaDestino", obj.CuentaDestino),
                new SqlParameter("@Fecha",obj.Fecha),
                new SqlParameter("@Monto",obj.Monto)
            };
            DAO dao = new DAO();
            int filaAfectada = dao.ExecuteSQLSEVER(spName, listParam);
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
            DataTable listado = null;
            string spName = "PilMoney_Api_ListadoDeTransacciones";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@cuentaPropia", 2),
            };
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName, listParam);
            return listado;
        }

        public int Modificar(Transaccion obj)
        {
            throw new NotImplementedException();
        }
    }
}