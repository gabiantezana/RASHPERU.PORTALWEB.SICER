using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MSS.TAWA.DA
{
    public class CentroCostosDA
    {
        // Listar CentroCostosNivel5
        public List<CentroCostosBE> ListarCentroCostos(int Id, int Tipo, int Tipo2)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;
            SqlParameter pTipo2;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_CentroCostosListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = Id;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pTipo2);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<CentroCostosBE> lstCentroCostosBE;
                CentroCostosBE objCentroCostosBE;
                lstCentroCostosBE = new List<CentroCostosBE>();

                while (sqlDR.Read())
                {
                    objCentroCostosBE = new CentroCostosBE();
                    objCentroCostosBE.IdCentroCostos = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos"));
                    objCentroCostosBE.Nivel = sqlDR.GetInt32(sqlDR.GetOrdinal("Nivel"));
                    objCentroCostosBE.CodigoSAP = sqlDR.GetString(sqlDR.GetOrdinal("CodigoSAP"));
                    objCentroCostosBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objCentroCostosBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objCentroCostosBE.Concepto = sqlDR.GetString(sqlDR.GetOrdinal("Concepto"));
                    objCentroCostosBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objCentroCostosBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objCentroCostosBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objCentroCostosBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstCentroCostosBE.Add(objCentroCostosBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstCentroCostosBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener CentroCostosNivel5
        public CentroCostosBE ObtenerCentroCostos(int CodigoSAP)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_CentroCostosObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = CodigoSAP;
                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                CentroCostosBE objCentroCostosBE;
                objCentroCostosBE = null;

                while (sqlDR.Read())
                {
                    objCentroCostosBE = new CentroCostosBE();
                    objCentroCostosBE.IdCentroCostos = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos"));
                    objCentroCostosBE.Nivel = sqlDR.GetInt32(sqlDR.GetOrdinal("Nivel"));
                    objCentroCostosBE.CodigoSAP = sqlDR.GetString(sqlDR.GetOrdinal("CodigoSAP"));
                    objCentroCostosBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objCentroCostosBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objCentroCostosBE.Concepto = sqlDR.GetString(sqlDR.GetOrdinal("Concepto"));
                    objCentroCostosBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objCentroCostosBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objCentroCostosBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objCentroCostosBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objCentroCostosBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
