using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BEPilMoney.AccesoADatos
{
    public class DAO
    {
        string _cadenaConexion; 

        public DAO()
        {
            _cadenaConexion = ConfigurationManager.ConnectionStrings["PilMoney"].ConnectionString;
        }

        public DataTable SelectDataBase(string spName, List<SqlParameter> parametros = null)
        {
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand cmd;
            DataTable dt;
            try
            {
                cnn.Open();
                cmd = new SqlCommand(spName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                if (parametros != null) cmd.Parameters.AddRange(parametros.ToArray());
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return dt;
        }
    }
}