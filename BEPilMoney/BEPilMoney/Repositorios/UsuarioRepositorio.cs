using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BEPilMoney.AccesoADatos;
using BEPilMoney.Models;

namespace BEPilMoney.Repositorios
{
	public class UsuarioRepositorio : IRepositorio<Usuario>
	{

        public DataTable Listado()
        {
            DataTable listado = null;
            string spName = "PilMoney_Api_ListadoDeUsuarios";
            listado = HelperSqlServer.GetHelperSqlServer().SelectDataBase(spName);
            return listado;
        }

        public DataTable Detalle(int id)
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

        public int Eliminar(int id)
        {
            string spName = "[PilMoney_Api_EliminarUsuario]";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@id", id)
            };
            int filaAfectada = HelperSqlServer.GetHelperSqlServer().ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }

        public int Agregar(Usuario obj)
        {
            string spName = "PilMoney_Api_AgregarUsuario";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@DNI", obj.DNI),
                new SqlParameter("@Nombre",obj.Nombre),
                new SqlParameter("@Apellido", obj.Apellido),
                new SqlParameter("@Email",obj.Email),
                new SqlParameter("@NombreUsuario",obj.NombreUsuario),
                new SqlParameter("@Clave",obj.Clave),
                new SqlParameter("@FotoPerfil",obj.FotoPerfil),
                new SqlParameter("@FotoDNI",obj.FotoDNI),
            };
            int filaAfectada = HelperSqlServer.GetHelperSqlServer().ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }

        public int Modificar(Usuario obj)
        {
            string spName = "[PilMoney_Api_ModificarUsuario]";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", obj.Id),
                new SqlParameter("@DNI", obj.DNI),
                new SqlParameter("@Nombre",obj.Nombre),
                new SqlParameter("@Apellido", obj.Apellido),
                new SqlParameter("@Email",obj.Email),
                new SqlParameter("@NombreUsuario",obj.NombreUsuario),
                new SqlParameter("@Clave",obj.Clave),
                new SqlParameter("@FotoPerfil",obj.FotoPerfil),
                new SqlParameter("@FotoDNI",obj.FotoDNI),
            };
            int filaAfectada = HelperSqlServer.GetHelperSqlServer().ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }
    }
}