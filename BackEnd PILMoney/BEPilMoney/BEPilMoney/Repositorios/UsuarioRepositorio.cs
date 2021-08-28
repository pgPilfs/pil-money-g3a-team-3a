using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BEPilMoney.AccesoADatos;

namespace BEPilMoney.Repositorios
{
    public class UsuarioRepositorio
    {
        public DataTable ListadoDeUsuario()
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_ListadoDeUsuarios";
            listado = HelperSqlServer.GetHelperSqlServer().SelectDataBase(spName);
            return listado;
        }

        public DataTable DetalleUsuario(int id)
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_DetalleDeUsuario";
            List<SqlParameter> listParam = new List<SqlParameter>() 
            { 
                new SqlParameter("@id", id) 
            };
            listado = HelperSqlServer.GetHelperSqlServer().SelectDataBase(spName, listParam);
            return listado;
        }
    }
}