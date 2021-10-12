using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
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
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName);
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
            DAO dao = new DAO();
            listado = dao.SelectDataBase(spName, listParam);
            return listado;
        }

        public int Eliminar(int id)
        {
            string spName = "[PilMoney_Api_EliminarUsuario]";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@id", id)
            };
            DAO dao = new DAO();
            int filaAfectada = dao.ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }

        public int Agregar(Usuario obj)
        {
            string spName = "PilMoney_Api_AgregarUsuario";
            Cuenta cuenta = new Cuenta(obj.NombreUsuario);
            byte[] bytes;
            if (obj.FotoPerfil != null)
                bytes = Encoding.UTF8.GetBytes(obj.FotoPerfil);
            else
                bytes = null;
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@DNI", obj.DNI),
                new SqlParameter("@Nombre",obj.Nombre),
                new SqlParameter("@Apellido", obj.Apellido),
                new SqlParameter("@Email",obj.Email),
                new SqlParameter("@NombreUsuario",obj.NombreUsuario),
                new SqlParameter("@Clave",this.GetSHA256(obj.Clave)),
                new SqlParameter("@FotoPerfil", bytes),
                new SqlParameter("@FotoDNI",obj.FotoDNI),
                new SqlParameter("@TipoCuenta", cuenta.TipoCuenta),
                new SqlParameter("@Usuario", cuenta.Usuario),
                new SqlParameter("@TipoMoneda", cuenta.TipoDeMoneda),
                new SqlParameter("@CVU", cuenta.CVU),
                new SqlParameter("@Alias", cuenta.Alias),
                new SqlParameter("@FechaAlta", cuenta.FechaAlta),
                new SqlParameter("@Saldo", cuenta.Saldo)

            };
            DAO dao = new DAO();
            int filaAfectada = dao.ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }

        public int Modificar(Usuario obj)
        {
            string spName = "[PilMoney_Api_ModificarUsuario]";
            byte[] bytes;
            bytes = Encoding.UTF8.GetBytes(obj.FotoPerfil);
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", obj.Id),
                new SqlParameter("@Nombre",obj.Nombre),
                new SqlParameter("@Apellido", obj.Apellido),
                new SqlParameter("@Email",obj.Email),
                new SqlParameter("@FotoPerfil", bytes),
                new SqlParameter("@FotoDNI",obj.FotoDNI),
            };
            DAO dao = new DAO();
            int filaAfectada = dao.ExecuteSQLSEVER(spName, listParam);
            return filaAfectada;
        }

        private string GetSHA256(string pass)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encodig = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encodig.GetBytes(pass));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}