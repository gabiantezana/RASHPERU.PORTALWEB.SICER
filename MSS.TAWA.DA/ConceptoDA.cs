using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class ConceptoDA
    {
        // Listar Concepto
        public List<ConceptoBE> ListarConcepto()
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
                strSP = "MSS_WEB_ConceptoListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<ConceptoBE> lstConceptoBE;
                ConceptoBE objConceptoBE;
                lstConceptoBE = new List<ConceptoBE>();

                while (sqlDR.Read())
                {
                    objConceptoBE = new ConceptoBE();
                    objConceptoBE.IdConcepto = sqlDR.GetString(sqlDR.GetOrdinal("IdConcepto"));
                    objConceptoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objConceptoBE.CuentaContable = sqlDR.GetString(sqlDR.GetOrdinal("CuentaContable"));
                    //objConceptoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    //objConceptoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    //objConceptoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    //objConceptoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstConceptoBE.Add(objConceptoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstConceptoBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Obtener Concepto
        public ConceptoBE ObtenerConcepto(string codigo)
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
                strSP = "MSS_WEB_ConceptoObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@codigo";
                pId.SqlDbType = SqlDbType.NVarChar;
                pId.Value = codigo;
                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                ConceptoBE objConceptoBE;
                objConceptoBE = null;

                while (sqlDR.Read())
                {
                    objConceptoBE = new ConceptoBE();
                    objConceptoBE.IdConcepto = sqlDR.GetString(sqlDR.GetOrdinal("U_Codigo"));
                    objConceptoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("U_Descripcion"));
                    objConceptoBE.CuentaContable = sqlDR.GetString(sqlDR.GetOrdinal("u_CuentaContable"));
                    //objConceptoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    //objConceptoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    //objConceptoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    //objConceptoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objConceptoBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}