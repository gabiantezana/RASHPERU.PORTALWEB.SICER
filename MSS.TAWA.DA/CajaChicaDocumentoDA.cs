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
    public class CajaChicaDocumentoDA
    {
        // Listar CajaChicaDocumento
        public List<CajaChicaDocumentoBE> ListarCajaChicaDocumento(int Id, int Tipo, int Tipo2)
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
                strSP = "MSS_WEB_CajaChicaDocumentoListar";
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

                List<CajaChicaDocumentoBE> lstCajaChicaDocumentoBE;
                CajaChicaDocumentoBE objCajaChicaDocumentoBE;
                lstCajaChicaDocumentoBE = new List<CajaChicaDocumentoBE>();

                while (sqlDR.Read())
                {
                    objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                    objCajaChicaDocumentoBE.IdCajaChicaDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChicaDocumento"));
                    objCajaChicaDocumentoBE.IdCajaChica = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));                    
                    objCajaChicaDocumentoBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objCajaChicaDocumentoBE.IdConcepto = sqlDR.GetInt32(sqlDR.GetOrdinal("IdConcepto"));
                    objCajaChicaDocumentoBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objCajaChicaDocumentoBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objCajaChicaDocumentoBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objCajaChicaDocumentoBE.Rendicion = sqlDR.GetInt32(sqlDR.GetOrdinal("Rendicion"));
                    objCajaChicaDocumentoBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
                    objCajaChicaDocumentoBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
                    objCajaChicaDocumentoBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
                    objCajaChicaDocumentoBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
                    objCajaChicaDocumentoBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
                    objCajaChicaDocumentoBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
                    objCajaChicaDocumentoBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
                    objCajaChicaDocumentoBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal"));                    
                    objCajaChicaDocumentoBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objCajaChicaDocumentoBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objCajaChicaDocumentoBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objCajaChicaDocumentoBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
                    objCajaChicaDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objCajaChicaDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objCajaChicaDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objCajaChicaDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objCajaChicaDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstCajaChicaDocumentoBE.Add(objCajaChicaDocumentoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstCajaChicaDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener CajaChicaDocumento
        public CajaChicaDocumentoBE ObtenerCajaChicaDocumento(int Id, int Tipo)
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
                strSP = "MSS_WEB_CajaChicaDocumentoObtener";
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

                CajaChicaDocumentoBE objCajaChicaDocumentoBE;
                objCajaChicaDocumentoBE = null;

                while (sqlDR.Read())
                {
                    objCajaChicaDocumentoBE = new CajaChicaDocumentoBE();
                    objCajaChicaDocumentoBE.IdCajaChicaDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChicaDocumento"));
                    objCajaChicaDocumentoBE.IdCajaChica = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));  
                    objCajaChicaDocumentoBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objCajaChicaDocumentoBE.IdConcepto = sqlDR.GetInt32(sqlDR.GetOrdinal("IdConcepto"));
                    objCajaChicaDocumentoBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objCajaChicaDocumentoBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objCajaChicaDocumentoBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objCajaChicaDocumentoBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
                    objCajaChicaDocumentoBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
                    objCajaChicaDocumentoBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
                    objCajaChicaDocumentoBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
                    objCajaChicaDocumentoBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
                    objCajaChicaDocumentoBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
                    objCajaChicaDocumentoBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
                    objCajaChicaDocumentoBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal")); 
                    objCajaChicaDocumentoBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objCajaChicaDocumentoBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objCajaChicaDocumentoBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objCajaChicaDocumentoBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
                    objCajaChicaDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objCajaChicaDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objCajaChicaDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objCajaChicaDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objCajaChicaDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objCajaChicaDocumentoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar CajaChicaDocumento
        public int InsertarCajaChicaDocumento(CajaChicaDocumentoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdCajaChicaDocumento;
            SqlParameter pIdCajaChica;
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
                strSP = "MSS_WEB_CajaChicaDocumentoInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdCajaChicaDocumento = new SqlParameter();
                pIdCajaChicaDocumento.Direction = ParameterDirection.ReturnValue;
                pIdCajaChicaDocumento.SqlDbType = SqlDbType.Int;

                pIdCajaChica = new SqlParameter();
                pIdCajaChica.ParameterName = "@IdCajaChica";
                pIdCajaChica.SqlDbType = SqlDbType.Int;
                pIdCajaChica.Value = objBE.IdCajaChica;

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

                sqlCmd.Parameters.Add(pIdCajaChicaDocumento);
                sqlCmd.Parameters.Add(pIdCajaChica);
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
                Id = Convert.ToInt32(pIdCajaChicaDocumento.Value);

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

        // Modificar CajaChicaDocumento
        public void ModificarCajaChicaDocumento(CajaChicaDocumentoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdCajaChicaDocumento;
            SqlParameter pIdCajaChica;
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

                strSP = "MSS_WEB_CajaChicaDocumentoModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdCajaChicaDocumento = new SqlParameter();
                pIdCajaChicaDocumento.ParameterName = "@IdCajaChicaDocumento";
                pIdCajaChicaDocumento.SqlDbType = SqlDbType.Int;
                pIdCajaChicaDocumento.Value = objBE.IdCajaChicaDocumento;

                pIdCajaChica = new SqlParameter();
                pIdCajaChica.ParameterName = "@IdCajaChica";
                pIdCajaChica.SqlDbType = SqlDbType.Int;
                pIdCajaChica.Value = objBE.IdCajaChica;

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

                sqlCmd.Parameters.Add(pIdCajaChicaDocumento);
                sqlCmd.Parameters.Add(pIdCajaChica);
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

        // Eliminar CajaChicaDocumento
        public void EliminarCajaChicaDocumento(int IdCajaChicaDocumento)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdCajaChicaDocumento;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_CajaChicaDocumentoEliminar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                
                pIdCajaChicaDocumento = new SqlParameter();
                pIdCajaChicaDocumento.ParameterName = "@IdCajaChicaDocumento";
                pIdCajaChicaDocumento.SqlDbType = SqlDbType.Int;
                pIdCajaChicaDocumento.Value = IdCajaChicaDocumento;

                sqlCmd.Parameters.Add(pIdCajaChicaDocumento);

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
