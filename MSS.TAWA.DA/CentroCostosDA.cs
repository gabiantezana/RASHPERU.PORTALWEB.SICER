using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class CentroCostosDA
    {
        // Listar CentroCostosNivel5
        public List<CentroCostosBE> ListarCentroCostos(int Nivel)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;
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
                pId.ParameterName = "@IdEmpresa";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = 1;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Nivel";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Nivel;

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pTipo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<CentroCostosBE> lstCentroCostosBE;
                CentroCostosBE objCentroCostosBE;
                lstCentroCostosBE = new List<CentroCostosBE>();

                while (sqlDR.Read())
                {
                    objCentroCostosBE = new CentroCostosBE();
                    objCentroCostosBE.IdCentroCostos = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos"));
                    objCentroCostosBE.Nivel = sqlDR.GetInt32(sqlDR.GetOrdinal("Nivel"));
                    objCentroCostosBE.CodigoSAP = sqlDR.GetString(sqlDR.GetOrdinal("CodigoSAP"));
                    if (!sqlDR.IsDBNull(sqlDR.GetOrdinal("Descripcion")))
                        objCentroCostosBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    else
                        objCentroCostosBE.Descripcion = string.Empty;
                    objCentroCostosBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    //objCentroCostosBE.Concepto = sqlDR.GetString(sqlDR.GetOrdinal("Concepto"));
                    //objCentroCostosBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    //objCentroCostosBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    //objCentroCostosBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    //objCentroCostosBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
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
                throw;
            }
        }

        // Obtener CentroCostosNivel5
        public CentroCostosBE ObtenerCentroCostos(string CodigoSAP)
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
                strSP = "MSS_WEB_CentroCostosObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@OcrCode";
                pId.SqlDbType = SqlDbType.NVarChar;
                pId.Value = CodigoSAP;
                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                CentroCostosBE objCentroCostosBE;
                objCentroCostosBE = null;

                while (sqlDR.Read())
                {
                    objCentroCostosBE = new CentroCostosBE();
                    objCentroCostosBE.IdCentroCostos = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos"));
                    objCentroCostosBE.Nivel = sqlDR.GetInt32(sqlDR.GetOrdinal("Nivel"));
                    objCentroCostosBE.CodigoSAP = sqlDR.GetString(sqlDR.GetOrdinal("CodigoSAP"));
                    objCentroCostosBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objCentroCostosBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objCentroCostosBE.Concepto = sqlDR.GetString(sqlDR.GetOrdinal("Concepto"));
                    //objCentroCostosBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    //objCentroCostosBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    //objCentroCostosBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    //objCentroCostosBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objCentroCostosBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}