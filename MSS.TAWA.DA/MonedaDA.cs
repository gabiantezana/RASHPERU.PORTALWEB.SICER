using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class MonedaDA
    {
        // Listar Area
        public List<MonedaBE> ListarMoneda(int? IdDocumentoWeb = null)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_MonedaListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@IdDocumentoWeb";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = IdDocumentoWeb;

                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<MonedaBE> lstMonedaBE;
                MonedaBE objMonedaBE;
                lstMonedaBE = new List<MonedaBE>();

                while (sqlDR.Read())
                {
                    objMonedaBE = new MonedaBE();
                    objMonedaBE.IdMoneda = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMoneda"));
                    objMonedaBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objMonedaBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objMonedaBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objMonedaBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objMonedaBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstMonedaBE.Add(objMonedaBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstMonedaBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Obtener Area
        public MonedaBE ObtenerMoneda(int Id)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_MonedaObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = Id;
                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                MonedaBE objMonedaBE;
                objMonedaBE = null;

                while (sqlDR.Read())
                {
                    objMonedaBE = new MonedaBE();
                    objMonedaBE.IdMoneda = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMoneda"));
                    objMonedaBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objMonedaBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objMonedaBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objMonedaBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objMonedaBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objMonedaBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}