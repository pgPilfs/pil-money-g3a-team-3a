using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BEPilMoney.AccesoADatos;
using BEPilMoney.Models;
using System.Security.Cryptography;
using System.Text;

namespace BEPilMoney.Repositorios
{
    public class BaseRepositorio
    {
        private DataTable _dt = new DataTable();

        public DataTable Login(string usuario, string password)
        {
            string spName = "PilMoney_Api_Login";
            //password = this.GetSHA256(password);
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Usuario", usuario),
                new SqlParameter("@Password", password)
            };
            DAO dao = new DAO();
            this._dt = dao.SelectDataBase(spName, listParam);
            return this._dt;
        }

        public bool ValidarToken(string token)
        {
            bool resp = false;
            string spName = "PilMoney_Api_Validar_Token";
            List<SqlParameter> listParam = new List<SqlParameter>()
            {
                new SqlParameter("@Token", token)
            };
            string tokenBD = HelperSqlServer.GetHelperSqlServer().SelectDataBase(spName, listParam).Rows[0]["Token"].ToString();
            if (token == tokenBD)
            {
                resp = true;
            }
            return resp;
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