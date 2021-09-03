using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BEPilMoney.AccesoADatos;
using BEPilMoney.Models;

namespace BEPilMoney.Repositorios
{
    public class BaseRepositorio
    {
        private DataTable _dt = new DataTable();
        private string token { get; set; }

        public string Login(string usuario, string password)
        {
            string spName = "PilMoney_Api_Login";
            List<SqlParameter> listPara = new List<SqlParameter>()
            {
                new SqlParameter("@Usuario", usuario),
                new SqlParameter("@Password", password)
            };
            this._dt = HelperSqlServer.GetHelperSqlServer().SelectDataBase(spName, listPara);
            this.token = this._dt.Rows[0]["Token"].ToString();
            return this.token;
        }

        public bool ValidarToken(string token)
        {
            bool resp = false;
            this.token = "ffb2bc6a-a83f-462c-9727-58e58ab7898e";
            if (token == this.token)
            {
                resp = true;
            }
            return resp;
        }
    }
}