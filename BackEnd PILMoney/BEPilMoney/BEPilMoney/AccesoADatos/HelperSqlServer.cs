using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BEPilMoney.AccesoADatos
{
    public class HelperSqlServer
    {
        #region Propiedades
        private string _cadenaConexion;
        private static HelperSqlServer _intancia = new HelperSqlServer();
        private SqlConnection _cnn;
        private SqlCommand _cmd;
        private string _spName;
        private DataTable _dt;
        private int _filasAfecctadas;
        #endregion

        #region Contructor
        private HelperSqlServer()
        {
            this._cadenaConexion = ConfigurationManager.ConnectionStrings["PilMoney"].ConnectionString;
        }
        #endregion

        #region Patron Singleton
        public static HelperSqlServer GetHelperSqlServer()
        {
            if (_intancia == null) _intancia = new HelperSqlServer();
            return _intancia;
        }
        #endregion

        #region Metodos Accesso s Datos
        public void Conectar()
        {
            this._cnn = new SqlConnection(this._cadenaConexion);
            if(this._cnn.State == ConnectionState.Closed)
            {
                using (this._cmd = new SqlCommand())
                {
                    this._dt = new DataTable();
                    this._cnn.Open();
                    this._cmd.Connection = _cnn;
                    this._cmd.CommandText = _spName;
                    this._cmd.CommandType = CommandType.StoredProcedure;
                }
            }
            else
            {
                this._dt = new DataTable();
                this._cmd.Connection = _cnn;
                this._cmd.CommandText = _spName;
                this._cmd.CommandType = CommandType.StoredProcedure;
            }
        }
        public void Desconectar()
        {
            if(this._cnn.State == ConnectionState.Open)
            {
                this._cnn.Close();
                this._cmd.Dispose();
                this._cnn.Dispose();
            }
        }
        #endregion

        #region Metodos CRUD SQL SERVER
        public DataTable SelectDataBase(string spName, List<SqlParameter> parametros = null)
        {
            try
            {
                this._spName = spName;
                this.Conectar();
                if (parametros != null) this._cmd.Parameters.AddRange(parametros.ToArray());
                this._dt.Load(this._cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
              this.Desconectar();
            }
            return this._dt;
        }
        public int ExecuteSQLSEVER(string spName, List<SqlParameter> parametros)
        {
            try
            {
                this._spName = spName;
                this.Conectar();
                this._cmd.Parameters.AddRange(parametros.ToArray());
                this._filasAfecctadas = this._cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Desconectar();
            }
            return this._filasAfecctadas;
        }
        #endregion
    }
}