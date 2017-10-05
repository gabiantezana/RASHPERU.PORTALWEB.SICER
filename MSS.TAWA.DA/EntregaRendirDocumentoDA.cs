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
    public class EntregaRendirDocumentoDA
    {
        // Listar EntregaRendirDocumento
        public List<EntregaRendirDocumentoBE> ListarEntregaRendirDocumento(int Id, int Tipo)
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
                strSP = "MSS_WEB_EntregaRendirDocumentoListar";
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

                List<EntregaRendirDocumentoBE> lstEntregaRendirDocumentoBE;
                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE;
                lstEntregaRendirDocumentoBE = new List<EntregaRendirDocumentoBE>();

                while (sqlDR.Read())
                {
                    objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                    objEntregaRendirDocumentoBE.IdEntregaRendirDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendirDocumento"));
                    objEntregaRendirDocumentoBE.IdEntregaRendir = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendir"));
                    objEntregaRendirDocumentoBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objEntregaRendirDocumentoBE.IdConcepto = sqlDR.GetInt32(sqlDR.GetOrdinal("IdConcepto"));
                    objEntregaRendirDocumentoBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objEntregaRendirDocumentoBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objEntregaRendirDocumentoBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objEntregaRendirDocumentoBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
                    objEntregaRendirDocumentoBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
                    objEntregaRendirDocumentoBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
                    objEntregaRendirDocumentoBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
                    objEntregaRendirDocumentoBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
                    objEntregaRendirDocumentoBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
                    objEntregaRendirDocumentoBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
                    objEntregaRendirDocumentoBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal"));
                    objEntregaRendirDocumentoBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objEntregaRendirDocumentoBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objEntregaRendirDocumentoBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objEntregaRendirDocumentoBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
                    objEntregaRendirDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objEntregaRendirDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objEntregaRendirDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objEntregaRendirDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objEntregaRendirDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstEntregaRendirDocumentoBE.Add(objEntregaRendirDocumentoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstEntregaRendirDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener EntregaRendirDocumento
        public EntregaRendirDocumentoBE ObtenerEntregaRendirDocumento(int Id, int Tipo)
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
                strSP = "MSS_WEB_EntregaRendirDocumentoObtener";
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

                EntregaRendirDocumentoBE objEntregaRendirDocumentoBE;
                objEntregaRendirDocumentoBE = null;

                while (sqlDR.Read())
                {
                    objEntregaRendirDocumentoBE = new EntregaRendirDocumentoBE();
                    objEntregaRendirDocumentoBE.IdEntregaRendirDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendirDocumento"));
                    objEntregaRendirDocumentoBE.IdEntregaRendir = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendir"));
                    objEntregaRendirDocumentoBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objEntregaRendirDocumentoBE.IdConcepto = sqlDR.GetInt32(sqlDR.GetOrdinal("IdConcepto"));
                    objEntregaRendirDocumentoBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objEntregaRendirDocumentoBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objEntregaRendirDocumentoBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objEntregaRendirDocumentoBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
                    objEntregaRendirDocumentoBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
                    objEntregaRendirDocumentoBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
                    objEntregaRendirDocumentoBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
                    objEntregaRendirDocumentoBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
                    objEntregaRendirDocumentoBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
                    objEntregaRendirDocumentoBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
                    objEntregaRendirDocumentoBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal"));
                    objEntregaRendirDocumentoBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objEntregaRendirDocumentoBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objEntregaRendirDocumentoBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objEntregaRendirDocumentoBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
                    objEntregaRendirDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objEntregaRendirDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objEntregaRendirDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objEntregaRendirDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objEntregaRendirDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objEntregaRendirDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar EntregaRendirDocumento
        public int InsertarEntregaRendirDocumento(EntregaRendirDocumentoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdEntregaRendirDocumento;
            SqlParameter pIdEntregaRendir;
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
                strSP = "MSS_WEB_EntregaRendirDocumentoInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdEntregaRendirDocumento = new SqlParameter();
                pIdEntregaRendirDocumento.Direction = ParameterDirection.ReturnValue;
                pIdEntregaRendirDocumento.SqlDbType = SqlDbType.Int;

                pIdEntregaRendir = new SqlParameter();
                pIdEntregaRendir.ParameterName = "@IdEntregaRendir";
                pIdEntregaRendir.SqlDbType = SqlDbType.Int;
                pIdEntregaRendir.Value = objBE.IdEntregaRendir;

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

                sqlCmd.Parameters.Add(pIdEntregaRendirDocumento);
                sqlCmd.Parameters.Add(pIdEntregaRendir);
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
                Id = Convert.ToInt32(pIdEntregaRendirDocumento.Value);

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

        // Modificar EntregaRendirDocumento
        public void ModificarEntregaRendirDocumento(EntregaRendirDocumentoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdEntregaRendirDocumento;
            SqlParameter pIdEntregaRendir;
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

                strSP = "MSS_WEB_EntregaRendirDocumentoModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdEntregaRendirDocumento = new SqlParameter();
                pIdEntregaRendirDocumento.ParameterName = "@IdEntregaRendirDocumento";
                pIdEntregaRendirDocumento.SqlDbType = SqlDbType.Int;
                pIdEntregaRendirDocumento.Value = objBE.IdEntregaRendirDocumento;

                pIdEntregaRendir = new SqlParameter();
                pIdEntregaRendir.ParameterName = "@IdEntregaRendir";
                pIdEntregaRendir.SqlDbType = SqlDbType.Int;
                pIdEntregaRendir.Value = objBE.IdEntregaRendir;

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

                sqlCmd.Parameters.Add(pIdEntregaRendirDocumento);
                sqlCmd.Parameters.Add(pIdEntregaRendir);
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

        // Eliminar EntregaRendirDocumento
        public void EliminarEntregaRendirDocumento(int IdEntregaRendirDocumento)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdEntregaRendirDocumento;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_EntregaRendirDocumentoEliminar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdEntregaRendirDocumento = new SqlParameter();
                pIdEntregaRendirDocumento.ParameterName = "@IdEntregaRendirDocumento";
                pIdEntregaRendirDocumento.SqlDbType = SqlDbType.Int;
                pIdEntregaRendirDocumento.Value = IdEntregaRendirDocumento;

                sqlCmd.Parameters.Add(pIdEntregaRendirDocumento);

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
