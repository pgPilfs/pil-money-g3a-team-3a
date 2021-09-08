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
            password = this.GetSHA256(password);
            List<SqlParameter> listPara = new List<SqlParameter>()
            {
                new SqlParameter("@Usuario", usuario),
                new SqlParameter("@Password", password)
            };
            this._dt = HelperSqlServer.GetHelperSqlServer().SelectDataBase(spName, listPara);
            return this._dt;
        }

        public bool ValidarToken(string token)
        {
            bool resp = false;
            if (token == "ffb2bc6a-a83f-462c-9727-58e58ab7898e")
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