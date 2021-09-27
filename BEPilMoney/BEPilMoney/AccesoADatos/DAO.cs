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

        public int ExecuteSQLSEVER(string spName, List<SqlParameter> parametros)
        {
            SqlConnection cnn = new SqlConnection(_cadenaConexion);
            SqlCommand cmd;
            int filasAfecctadas = 0;
            try
            {
                cnn.Open();
                cmd = new SqlCommand(spName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametros.ToArray());
                filasAfecctadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return filasAfecctadas;
        }

    }
}