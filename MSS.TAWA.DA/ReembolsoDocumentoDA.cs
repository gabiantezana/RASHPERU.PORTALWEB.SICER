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
    public class ReembolsoDocumentoDA
    {
        // Listar ReembolsoDocumento
        public List<ReembolsoDocumentoBE> ListarReembolsoDocumento(int Id, int Tipo)
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
                strSP = "MSS_WEB_ReembolsoDocumentoListar";
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

                List<ReembolsoDocumentoBE> lstReembolsoDocumentoBE;
                ReembolsoDocumentoBE objReembolsoDocumentoBE;
                lstReembolsoDocumentoBE = new List<ReembolsoDocumentoBE>();

                while (sqlDR.Read())
                {
                    objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                    objReembolsoDocumentoBE.IdReembolsoDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolsoDocumento"));
                    objReembolsoDocumentoBE.IdReembolso = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolso"));
                    objReembolsoDocumentoBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objReembolsoDocumentoBE.IdConcepto = sqlDR.GetInt32(sqlDR.GetOrdinal("IdConcepto"));
                    objReembolsoDocumentoBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objReembolsoDocumentoBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objReembolsoDocumentoBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objReembolsoDocumentoBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
                    objReembolsoDocumentoBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
                    objReembolsoDocumentoBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
                    objReembolsoDocumentoBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
                    objReembolsoDocumentoBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
                    objReembolsoDocumentoBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
                    objReembolsoDocumentoBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
                    objReembolsoDocumentoBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal"));
                    objReembolsoDocumentoBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objReembolsoDocumentoBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objReembolsoDocumentoBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objReembolsoDocumentoBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
                    objReembolsoDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objReembolsoDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objReembolsoDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objReembolsoDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objReembolsoDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstReembolsoDocumentoBE.Add(objReembolsoDocumentoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstReembolsoDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener ReembolsoDocumento
        public ReembolsoDocumentoBE ObtenerReembolsoDocumento(int Id, int Tipo)
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
                strSP = "MSS_WEB_ReembolsoDocumentoObtener";
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

                ReembolsoDocumentoBE objReembolsoDocumentoBE;
                objReembolsoDocumentoBE = null;

                while (sqlDR.Read())
                {
                    objReembolsoDocumentoBE = new ReembolsoDocumentoBE();
                    objReembolsoDocumentoBE.IdReembolsoDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolsoDocumento"));
                    objReembolsoDocumentoBE.IdReembolso = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolso"));
                    objReembolsoDocumentoBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objReembolsoDocumentoBE.IdConcepto = sqlDR.GetInt32(sqlDR.GetOrdinal("IdConcepto"));
                    objReembolsoDocumentoBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objReembolsoDocumentoBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objReembolsoDocumentoBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objReembolsoDocumentoBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
                    objReembolsoDocumentoBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
                    objReembolsoDocumentoBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
                    objReembolsoDocumentoBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
                    objReembolsoDocumentoBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
                    objReembolsoDocumentoBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
                    objReembolsoDocumentoBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
                    objReembolsoDocumentoBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal"));
                    objReembolsoDocumentoBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objReembolsoDocumentoBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objReembolsoDocumentoBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objReembolsoDocumentoBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
                    objReembolsoDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objReembolsoDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objReembolsoDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objReembolsoDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objReembolsoDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objReembolsoDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar ReembolsoDocumento
        public int InsertarReembolsoDocumento(ReembolsoDocumentoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdReembolsoDocumento;
            SqlParameter pIdReembolso;
            SqlParameter pIdProveedor;
            SqlParameter pIdConcepto;
            SqlParameter pIdCentroCostos3;
            SqlParameter pIdCentroCostos4;
            SqlParameter pIdCentroCostos5;
            SqlParameter pTipoDoc;
            SqlParameter pSerieDoc;
            SqlParameter pCorrelativoDoc;
            SqlParameter pFechaDoc;
            SqlParameter pIdMonedaDoc;
            SqlParameter pMontoDoc;
            SqlParameter pTasaCambio;
            SqlParameter pIdMonedaOriginal;
            SqlParameter pMontoNoAfecto;
            SqlParameter pMontoAfecto;
            SqlParameter pMontoIGV;
            SqlParameter pMontoTotal;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_ReembolsoDocumentoInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdReembolsoDocumento = new SqlParameter();
                pIdReembolsoDocumento.Direction = ParameterDirection.ReturnValue;
                pIdReembolsoDocumento.SqlDbType = SqlDbType.Int;

                pIdReembolso = new SqlParameter();
                pIdReembolso.ParameterName = "@IdReembolso";
                pIdReembolso.SqlDbType = SqlDbType.Int;
                pIdReembolso.Value = objBE.IdReembolso;

                pIdProveedor = new SqlParameter();
                pIdProveedor.ParameterName = "@IdProveedor";
                pIdProveedor.SqlDbType = SqlDbType.Int;
                pIdProveedor.Value = objBE.IdProveedor;

                pIdConcepto = new SqlParameter();
                pIdConcepto.ParameterName = "@IdConcepto";
                pIdConcepto.SqlDbType = SqlDbType.Int;
                pIdConcepto.Value = objBE.IdConcepto;

                pIdCentroCostos3 = new SqlParameter();
                pIdCentroCostos3.ParameterName = "@IdCentroCostos3";
                pIdCentroCostos3.SqlDbType = SqlDbType.Int;
                pIdCentroCostos3.Value = objBE.IdCentroCostos3;

                pIdCentroCostos4 = new SqlParameter();
                pIdCentroCostos4.ParameterName = "@IdCentroCostos4";
                pIdCentroCostos4.SqlDbType = SqlDbType.Int;
                pIdCentroCostos4.Value = objBE.IdCentroCostos4;

                pIdCentroCostos5 = new SqlParameter();
                pIdCentroCostos5.ParameterName = "@IdCentroCostos5";
                pIdCentroCostos5.SqlDbType = SqlDbType.Int;
                pIdCentroCostos5.Value = objBE.IdCentroCostos5;

                pTipoDoc = new SqlParameter();
                pTipoDoc.ParameterName = "@TipoDoc";
                pTipoDoc.SqlDbType = SqlDbType.VarChar;
                pTipoDoc.Size = 3;
                pTipoDoc.Value = objBE.TipoDoc;

                pSerieDoc = new SqlParameter();
                pSerieDoc.ParameterName = "@SerieDoc";
                pSerieDoc.SqlDbType = SqlDbType.VarChar;
                pSerieDoc.Size = 10;
                pSerieDoc.Value = objBE.SerieDoc;

                pCorrelativoDoc = new SqlParameter();
                pCorrelativoDoc.ParameterName = "@CorrelativoDoc";
                pCorrelativoDoc.SqlDbType = SqlDbType.VarChar;
                pCorrelativoDoc.Size = 20;
                pCorrelativoDoc.Value = objBE.CorrelativoDoc;

                pFechaDoc = new SqlParameter();
                pFechaDoc.ParameterName = "@FechaDoc";
                pFechaDoc.SqlDbType = SqlDbType.DateTime;
                pFechaDoc.Value = objBE.FechaDoc;

                pIdMonedaDoc = new SqlParameter();
                pIdMonedaDoc.ParameterName = "@IdMonedaDoc";
                pIdMonedaDoc.SqlDbType = SqlDbType.Int;
                pIdMonedaDoc.Value = objBE.IdMonedaDoc;

                pMontoDoc = new SqlParameter();
                pMontoDoc.ParameterName = "@MontoDoc";
                pMontoDoc.SqlDbType = SqlDbType.VarChar;
                pMontoDoc.Size = 20;
                pMontoDoc.Value = objBE.MontoDoc;

                pTasaCambio = new SqlParameter();
                pTasaCambio.ParameterName = "@TasaCambio";
                pTasaCambio.SqlDbType = SqlDbType.VarChar;
                pTasaCambio.Size = 20;
                pTasaCambio.Value = objBE.TasaCambio;

                pIdMonedaOriginal = new SqlParameter();
                pIdMonedaOriginal.ParameterName = "@IdMonedaOriginal";
                pIdMonedaOriginal.SqlDbType = SqlDbType.Int;
                pIdMonedaOriginal.Value = objBE.IdMonedaOriginal;

                pMontoNoAfecto = new SqlParameter();
                pMontoNoAfecto.ParameterName = "@MontoNoAfecto";
                pMontoNoAfecto.SqlDbType = SqlDbType.VarChar;
                pMontoNoAfecto.Size = 20;
                pMontoNoAfecto.Value = objBE.MontoNoAfecto;

                pMontoAfecto = new SqlParameter();
                pMontoAfecto.ParameterName = "@MontoAfecto";
                pMontoAfecto.SqlDbType = SqlDbType.VarChar;
                pMontoAfecto.Size = 20;
                pMontoAfecto.Value = objBE.MontoAfecto;

                pMontoIGV = new SqlParameter();
                pMontoIGV.ParameterName = "@MontoIGV";
                pMontoIGV.SqlDbType = SqlDbType.VarChar;
                pMontoIGV.Size = 20;
                pMontoIGV.Value = objBE.MontoIGV;

                pMontoTotal = new SqlParameter();
                pMontoTotal.ParameterName = "@MontoTotal";
                pMontoTotal.SqlDbType = SqlDbType.VarChar;
                pMontoTotal.Size = 20;
                pMontoTotal.Value = objBE.MontoTotal;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.VarChar;
                pEstado.Size = 3;
                pEstado.Value = objBE.Estado;

                pUserCreate = new SqlParameter();
                pUserCreate.ParameterName = "@UserCreate";
                pUserCreate.SqlDbType = SqlDbType.VarChar;
                pUserCreate.Size = 20;
                pUserCreate.Value = objBE.UserCreate;

                pCreateDate = new SqlParameter();
                pCreateDate.ParameterName = "@CreateDate";
                pCreateDate.SqlDbType = SqlDbType.DateTime;
                pCreateDate.Value = objBE.CreateDate;

                pUserUpdate = new SqlParameter();
                pUserUpdate.ParameterName = "@UserUpdate";
                pUserUpdate.SqlDbType = SqlDbType.VarChar;
                pUserUpdate.Size = 20;
                pUserUpdate.Value = objBE.UserUpdate;

                pUpdateDate = new SqlParameter();
                pUpdateDate.ParameterName = "@UpdateDate";
                pUpdateDate.SqlDbType = SqlDbType.DateTime;
                pUpdateDate.Value = objBE.UpdateDate;

                sqlCmd.Parameters.Add(pIdReembolsoDocumento);
                sqlCmd.Parameters.Add(pIdReembolso);
                sqlCmd.Parameters.Add(pIdProveedor);
                sqlCmd.Parameters.Add(pIdConcepto);
                sqlCmd.Parameters.Add(pIdCentroCostos3);
                sqlCmd.Parameters.Add(pIdCentroCostos4);
                sqlCmd.Parameters.Add(pIdCentroCostos5);
                sqlCmd.Parameters.Add(pTipoDoc);
                sqlCmd.Parameters.Add(pSerieDoc);
                sqlCmd.Parameters.Add(pCorrelativoDoc);
                sqlCmd.Parameters.Add(pFechaDoc);
                sqlCmd.Parameters.Add(pIdMonedaDoc);
                sqlCmd.Parameters.Add(pMontoDoc);
                sqlCmd.Parameters.Add(pTasaCambio);
                sqlCmd.Parameters.Add(pIdMonedaOriginal);
                sqlCmd.Parameters.Add(pMontoNoAfecto);
                sqlCmd.Parameters.Add(pMontoAfecto);
                sqlCmd.Parameters.Add(pMontoIGV);
                sqlCmd.Parameters.Add(pMontoTotal);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdReembolsoDocumento.Value);

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();

                return Id;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Modificar ReembolsoDocumento
        public void ModificarReembolsoDocumento(ReembolsoDocumentoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdReembolsoDocumento;
            SqlParameter pIdReembolso;
            SqlParameter pIdProveedor;
            SqlParameter pIdConcepto;
            SqlParameter pIdCentroCostos3;
            SqlParameter pIdCentroCostos4;
            SqlParameter pIdCentroCostos5;
            SqlParameter pTipoDoc;
            SqlParameter pSerieDoc;
            SqlParameter pCorrelativoDoc;
            SqlParameter pFechaDoc;
            SqlParameter pIdMonedaDoc;
            SqlParameter pMontoDoc;
            SqlParameter pTasaCambio;
            SqlParameter pIdMonedaOriginal;
            SqlParameter pMontoNoAfecto;
            SqlParameter pMontoAfecto;
            SqlParameter pMontoIGV;
            SqlParameter pMontoTotal;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_ReembolsoDocumentoModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdReembolsoDocumento = new SqlParameter();
                pIdReembolsoDocumento.ParameterName = "@IdReembolsoDocumento";
                pIdReembolsoDocumento.SqlDbType = SqlDbType.Int;
                pIdReembolsoDocumento.Value = objBE.IdReembolsoDocumento;

                pIdReembolso = new SqlParameter();
                pIdReembolso.ParameterName = "@IdReembolso";
                pIdReembolso.SqlDbType = SqlDbType.Int;
                pIdReembolso.Value = objBE.IdReembolso;

                pIdProveedor = new SqlParameter();
                pIdProveedor.ParameterName = "@IdProveedor";
                pIdProveedor.SqlDbType = SqlDbType.Int;
                pIdProveedor.Value = objBE.IdProveedor;

                pIdConcepto = new SqlParameter();
                pIdConcepto.ParameterName = "@IdConcepto";
                pIdConcepto.SqlDbType = SqlDbType.Int;
                pIdConcepto.Value = objBE.IdConcepto;

                pIdCentroCostos3 = new SqlParameter();
                pIdCentroCostos3.ParameterName = "@IdCentroCostos3";
                pIdCentroCostos3.SqlDbType = SqlDbType.Int;
                pIdCentroCostos3.Value = objBE.IdCentroCostos3;

                pIdCentroCostos4 = new SqlParameter();
                pIdCentroCostos4.ParameterName = "@IdCentroCostos4";
                pIdCentroCostos4.SqlDbType = SqlDbType.Int;
                pIdCentroCostos4.Value = objBE.IdCentroCostos4;

                pIdCentroCostos5 = new SqlParameter();
                pIdCentroCostos5.ParameterName = "@IdCentroCostos5";
                pIdCentroCostos5.SqlDbType = SqlDbType.Int;
                pIdCentroCostos5.Value = objBE.IdCentroCostos5;

                pTipoDoc = new SqlParameter();
                pTipoDoc.ParameterName = "@TipoDoc";
                pTipoDoc.SqlDbType = SqlDbType.VarChar;
                pTipoDoc.Size = 3;
                pTipoDoc.Value = objBE.TipoDoc;

                pSerieDoc = new SqlParameter();
                pSerieDoc.ParameterName = "@SerieDoc";
                pSerieDoc.SqlDbType = SqlDbType.VarChar;
                pSerieDoc.Size = 10;
                pSerieDoc.Value = objBE.SerieDoc;

                pCorrelativoDoc = new SqlParameter();
                pCorrelativoDoc.ParameterName = "@CorrelativoDoc";
                pCorrelativoDoc.SqlDbType = SqlDbType.VarChar;
                pCorrelativoDoc.Size = 20;
                pCorrelativoDoc.Value = objBE.CorrelativoDoc;

                pFechaDoc = new SqlParameter();
                pFechaDoc.ParameterName = "@FechaDoc";
                pFechaDoc.SqlDbType = SqlDbType.DateTime;
                pFechaDoc.Value = objBE.FechaDoc;

                pIdMonedaDoc = new SqlParameter();
                pIdMonedaDoc.ParameterName = "@IdMonedaDoc";
                pIdMonedaDoc.SqlDbType = SqlDbType.Int;
                pIdMonedaDoc.Value = objBE.IdMonedaDoc;

                pMontoDoc = new SqlParameter();
                pMontoDoc.ParameterName = "@MontoDoc";
                pMontoDoc.SqlDbType = SqlDbType.VarChar;
                pMontoDoc.Size = 20;
                pMontoDoc.Value = objBE.MontoDoc;

                pTasaCambio = new SqlParameter();
                pTasaCambio.ParameterName = "@TasaCambio";
                pTasaCambio.SqlDbType = SqlDbType.VarChar;
                pTasaCambio.Size = 20;
                pTasaCambio.Value = objBE.TasaCambio;

                pIdMonedaOriginal = new SqlParameter();
                pIdMonedaOriginal.ParameterName = "@IdMonedaOriginal";
                pIdMonedaOriginal.SqlDbType = SqlDbType.Int;
                pIdMonedaOriginal.Value = objBE.IdMonedaOriginal;

                pMontoNoAfecto = new SqlParameter();
                pMontoNoAfecto.ParameterName = "@MontoNoAfecto";
                pMontoNoAfecto.SqlDbType = SqlDbType.VarChar;
                pMontoNoAfecto.Size = 20;
                pMontoNoAfecto.Value = objBE.MontoNoAfecto;

                pMontoAfecto = new SqlParameter();
                pMontoAfecto.ParameterName = "@MontoAfecto";
                pMontoAfecto.SqlDbType = SqlDbType.VarChar;
                pMontoAfecto.Size = 20;
                pMontoAfecto.Value = objBE.MontoAfecto;

                pMontoIGV = new SqlParameter();
                pMontoIGV.ParameterName = "@MontoIGV";
                pMontoIGV.SqlDbType = SqlDbType.VarChar;
                pMontoIGV.Size = 20;
                pMontoIGV.Value = objBE.MontoIGV;

                pMontoTotal = new SqlParameter();
                pMontoTotal.ParameterName = "@MontoTotal";
                pMontoTotal.SqlDbType = SqlDbType.VarChar;
                pMontoTotal.Size = 20;
                pMontoTotal.Value = objBE.MontoTotal;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.VarChar;
                pEstado.Size = 3;
                pEstado.Value = objBE.Estado;

                pUserCreate = new SqlParameter();
                pUserCreate.ParameterName = "@UserCreate";
                pUserCreate.SqlDbType = SqlDbType.VarChar;
                pUserCreate.Size = 20;
                pUserCreate.Value = objBE.UserCreate;

                pCreateDate = new SqlParameter();
                pCreateDate.ParameterName = "@CreateDate";
                pCreateDate.SqlDbType = SqlDbType.DateTime;
                pCreateDate.Value = objBE.CreateDate;

                pUserUpdate = new SqlParameter();
                pUserUpdate.ParameterName = "@UserUpdate";
                pUserUpdate.SqlDbType = SqlDbType.VarChar;
                pUserUpdate.Size = 20;
                pUserUpdate.Value = objBE.UserUpdate;

                pUpdateDate = new SqlParameter();
                pUpdateDate.ParameterName = "@UpdateDate";
                pUpdateDate.SqlDbType = SqlDbType.DateTime;
                pUpdateDate.Value = objBE.UpdateDate;

                sqlCmd.Parameters.Add(pIdReembolsoDocumento);
                sqlCmd.Parameters.Add(pIdReembolso);
                sqlCmd.Parameters.Add(pIdProveedor);
                sqlCmd.Parameters.Add(pIdConcepto);
                sqlCmd.Parameters.Add(pIdCentroCostos3);
                sqlCmd.Parameters.Add(pIdCentroCostos4);
                sqlCmd.Parameters.Add(pIdCentroCostos5);
                sqlCmd.Parameters.Add(pTipoDoc);
                sqlCmd.Parameters.Add(pSerieDoc);
                sqlCmd.Parameters.Add(pCorrelativoDoc);
                sqlCmd.Parameters.Add(pFechaDoc);
                sqlCmd.Parameters.Add(pIdMonedaDoc);
                sqlCmd.Parameters.Add(pMontoDoc);
                sqlCmd.Parameters.Add(pTasaCambio);
                sqlCmd.Parameters.Add(pIdMonedaOriginal);
                sqlCmd.Parameters.Add(pMontoNoAfecto);
                sqlCmd.Parameters.Add(pMontoAfecto);
                sqlCmd.Parameters.Add(pMontoIGV);
                sqlCmd.Parameters.Add(pMontoTotal);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar ReembolsoDocumento
        public void EliminarReembolsoDocumento(int IdReembolsoDocumento)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdReembolsoDocumento;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_ReembolsoDocumentoEliminar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdReembolsoDocumento = new SqlParameter();
                pIdReembolsoDocumento.ParameterName = "@IdReembolsoDocumento";
                pIdReembolsoDocumento.SqlDbType = SqlDbType.Int;
                pIdReembolsoDocumento.Value = IdReembolsoDocumento;

                sqlCmd.Parameters.Add(pIdReembolsoDocumento);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
