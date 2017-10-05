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
    public class DocumentoDA
    {
        // Listar Documento
        public List<DocumentoBE> ListarDocumento(int Id, int Tipo)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_DocumentoListar";
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

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pTipo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<DocumentoBE> lstDocumentoBE;
                DocumentoBE objDocumentoBE;
                lstDocumentoBE = new List<DocumentoBE>();

                while (sqlDR.Read())
                {
                    objDocumentoBE = new DocumentoBE();
                    objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdDocumento"));
                    objDocumentoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objDocumentoBE.CodigoSunat = sqlDR.GetString(sqlDR.GetOrdinal("CodigoSunat"));
                    objDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstDocumentoBE.Add(objDocumentoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Documento
        public DocumentoBE ObtenerDocumento(int Id)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdDocumento;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_DocumentoObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdDocumento = new SqlParameter();
                pIdDocumento.ParameterName = "@IdDocumento";
                pIdDocumento.SqlDbType = SqlDbType.Int;
                pIdDocumento.Value = Id;
                sqlCmd.Parameters.Add(pIdDocumento);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                DocumentoBE objDocumentoBE;
                objDocumentoBE = null;

                while (sqlDR.Read())
                {
                    objDocumentoBE = new DocumentoBE();
                    objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdDocumento"));
                    objDocumentoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objDocumentoBE.CodigoSunat = sqlDR.GetString(sqlDR.GetOrdinal("CodigoSunat"));
                    objDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
