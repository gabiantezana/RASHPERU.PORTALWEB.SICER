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
    public class CajaChicaDA
    {
        // Listar CajaChica
        public List<DocumentBE> ListarDocumentos(int IdUsuario, int Tipo, int Tipo2, String CodigoDocumento, String Dni, String NombreSolicitante, String EsFacturable, String Estado)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdUsuario;
            SqlParameter pTipo;
            SqlParameter pTipo2;
            SqlParameter pCodigoDocumento;
            SqlParameter pDni;
            SqlParameter pNombreSolicitante;
            SqlParameter pEsFacturable;
            SqlParameter pEstado;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_CajaChicaListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = IdUsuario;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                pCodigoDocumento = new SqlParameter();
                pCodigoDocumento.ParameterName = "@CodigoDocumento";
                pCodigoDocumento.SqlDbType = SqlDbType.VarChar;
                pCodigoDocumento.Value = CodigoDocumento;

                pDni = new SqlParameter();
                pDni.ParameterName = "@Dni";
                pDni.SqlDbType = SqlDbType.VarChar;
                pDni.Value = Dni;

                pNombreSolicitante = new SqlParameter();
                pNombreSolicitante.ParameterName = "@NombreSolicitante";
                pNombreSolicitante.SqlDbType = SqlDbType.VarChar;
                pNombreSolicitante.Value = NombreSolicitante;

                pEsFacturable = new SqlParameter();
                pEsFacturable.ParameterName = "@EsFacturable";
                pEsFacturable.SqlDbType = SqlDbType.VarChar;
                pEsFacturable.Value = EsFacturable;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.VarChar;
                pEstado.Value = Estado;

                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pTipo2);
                sqlCmd.Parameters.Add(pCodigoDocumento);
                sqlCmd.Parameters.Add(pDni);
                sqlCmd.Parameters.Add(pNombreSolicitante);
                sqlCmd.Parameters.Add(pEsFacturable);
                sqlCmd.Parameters.Add(pEstado);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<DocumentBE> lstDocumentosBE;
                DocumentBE objDocumentoBE;
                lstDocumentosBE = new List<DocumentBE>();

                while (sqlDR.Read())
                {
                    objDocumentoBE = new DocumentBE();
                    objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));
                    objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoCajaChica"));
                    objDocumentoBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objDocumentoBE.IdArea = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea"));
                    objDocumentoBE.IdUsuarioCreador = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCreador"));
                    objDocumentoBE.IdUsuarioSolicitante = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioSolicitante"));
                    objDocumentoBE.IdCentroCostos1 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos1"));
                    objDocumentoBE.IdCentroCostos2 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos2"));
                    objDocumentoBE.IdCentroCostos3 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objDocumentoBE.IdCentroCostos4 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objDocumentoBE.IdCentroCostos5 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objDocumentoBE.IdMetodoPago = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMetodoPago"));
                    objDocumentoBE.MontoInicial = sqlDR.GetString(sqlDR.GetOrdinal("MontoInicial"));
                    objDocumentoBE.MontoGastado = sqlDR.GetString(sqlDR.GetOrdinal("MontoGastado"));
                    objDocumentoBE.MontoActual = sqlDR.GetString(sqlDR.GetOrdinal("MontoActual"));
                    objDocumentoBE.Moneda = sqlDR.GetString(sqlDR.GetOrdinal("Moneda"));
                    objDocumentoBE.EsFacturable = sqlDR.GetString(sqlDR.GetOrdinal("EsFacturable"));
                    objDocumentoBE.MomentoFacturable = sqlDR.GetString(sqlDR.GetOrdinal("MomentoFacturable"));
                    objDocumentoBE.Asunto = sqlDR.GetString(sqlDR.GetOrdinal("Asunto"));
                    objDocumentoBE.Comentario = sqlDR.GetString(sqlDR.GetOrdinal("Comentario"));
                    objDocumentoBE.MotivoDetalle = sqlDR.GetString(sqlDR.GetOrdinal("MotivoDetalle"));
                    objDocumentoBE.FechaSolicitud = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaSolicitud"));
                    objDocumentoBE.FechaContabilizacion = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaContabilizacion"));
                    objDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objDocumentoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objDocumentoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objDocumentoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objDocumentoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstDocumentosBE.Add(objDocumentoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstDocumentosBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Obtener CajaChica
        public DocumentBE ObtenerDocumento(int IdDocumento, int Tipo)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdDocumento;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_CajaChicaObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdDocumento = new SqlParameter();
                pIdDocumento.ParameterName = "@IdCajaChica";
                pIdDocumento.SqlDbType = SqlDbType.Int;
                pIdDocumento.Value = IdDocumento;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                sqlCmd.Parameters.Add(pIdDocumento);
                sqlCmd.Parameters.Add(pTipo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                DocumentBE objDocumentoBE;
                objDocumentoBE = null;

                while (sqlDR.Read())
                {
                    objDocumentoBE = new DocumentBE();
                    objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));
                    objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoCajaChica"));
                    objDocumentoBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objDocumentoBE.IdArea = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea"));
                    objDocumentoBE.IdUsuarioCreador = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCreador"));
                    objDocumentoBE.IdUsuarioSolicitante = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioSolicitante"));
                    objDocumentoBE.IdCentroCostos1 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos1"));
                    objDocumentoBE.IdCentroCostos2 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos2"));
                    objDocumentoBE.IdCentroCostos3 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objDocumentoBE.IdCentroCostos4 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objDocumentoBE.IdCentroCostos5 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objDocumentoBE.IdMetodoPago = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMetodoPago"));
                    objDocumentoBE.MontoInicial = sqlDR.GetString(sqlDR.GetOrdinal("MontoInicial"));
                    objDocumentoBE.MontoGastado = sqlDR.GetString(sqlDR.GetOrdinal("MontoGastado"));
                    objDocumentoBE.MontoActual = sqlDR.GetString(sqlDR.GetOrdinal("MontoActual"));
                    objDocumentoBE.Moneda = sqlDR.GetString(sqlDR.GetOrdinal("Moneda"));
                    objDocumentoBE.EsFacturable = sqlDR.GetString(sqlDR.GetOrdinal("EsFacturable"));
                    objDocumentoBE.MomentoFacturable = sqlDR.GetString(sqlDR.GetOrdinal("MomentoFacturable"));
                    objDocumentoBE.Asunto = sqlDR.GetString(sqlDR.GetOrdinal("Asunto"));
                    objDocumentoBE.Comentario = sqlDR.GetString(sqlDR.GetOrdinal("Comentario"));
                    objDocumentoBE.MotivoDetalle = sqlDR.GetString(sqlDR.GetOrdinal("MotivoDetalle"));
                    objDocumentoBE.FechaSolicitud = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaSolicitud"));
                    objDocumentoBE.FechaContabilizacion = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaContabilizacion"));
                    objDocumentoBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
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
                throw;
            }
        }

        // Insertar CajaChica
        public int InsertarDocumento(DocumentBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdDocumento;
            SqlParameter pCodigoDocumento;
            SqlParameter pIdEmpresa;
            SqlParameter pIdArea;
            SqlParameter pIdUsuarioCreador;
            SqlParameter pIdUsuarioSolicitante;
            SqlParameter pIdCentroCostos1;
            SqlParameter pIdCentroCostos2;
            SqlParameter pIdCentroCostos3;
            SqlParameter pIdCentroCostos4;
            SqlParameter pIdCentroCostos5;
            SqlParameter pIdMetodoPago;
            SqlParameter pMontoInicial;
            SqlParameter pMontoGastado;
            SqlParameter pMontoActual;
            SqlParameter pMoneda;
            SqlParameter pEsFacturable;
            SqlParameter pMomentoFacturable;
            SqlParameter pAsunto;
            SqlParameter pComentario;
            SqlParameter pMotivoDetalle;
            SqlParameter pFechaSolicitud;
            SqlParameter pFechaContabilizacion;
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
                strSP = "MSS_WEB_CajaChicaInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdDocumento = new SqlParameter();
                pIdDocumento.Direction = ParameterDirection.ReturnValue;
                pIdDocumento.SqlDbType = SqlDbType.Int;

                pCodigoDocumento = new SqlParameter();
                pCodigoDocumento.ParameterName = "@CodigoCajaChica";
                pCodigoDocumento.SqlDbType = SqlDbType.VarChar;
                pCodigoDocumento.Size = 100;
                pCodigoDocumento.Value = objBE.CodigoDocumento;

                pIdEmpresa = new SqlParameter();
                pIdEmpresa.ParameterName = "@IdEmpresa";
                pIdEmpresa.SqlDbType = SqlDbType.Int;
                pIdEmpresa.Value = objBE.IdEmpresa;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdArea";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = objBE.IdArea;

                pIdUsuarioCreador = new SqlParameter();
                pIdUsuarioCreador.ParameterName = "@IdUsuarioCreador";
                pIdUsuarioCreador.SqlDbType = SqlDbType.Int;
                pIdUsuarioCreador.Value = objBE.IdUsuarioCreador;

                pIdUsuarioSolicitante = new SqlParameter();
                pIdUsuarioSolicitante.ParameterName = "@IdUsuarioSolicitante";
                pIdUsuarioSolicitante.SqlDbType = SqlDbType.Int;
                pIdUsuarioSolicitante.Value = objBE.IdUsuarioSolicitante;

                pIdCentroCostos1 = new SqlParameter();
                pIdCentroCostos1.ParameterName = "@IdCentroCostos1";
                pIdCentroCostos1.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos1.Value = objBE.IdCentroCostos1;

                pIdCentroCostos2 = new SqlParameter();
                pIdCentroCostos2.ParameterName = "@IdCentroCostos2";
                pIdCentroCostos2.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos2.Value = objBE.IdCentroCostos2;

                pIdCentroCostos3 = new SqlParameter();
                pIdCentroCostos3.ParameterName = "@IdCentroCostos3";
                pIdCentroCostos3.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos3.Value = objBE.IdCentroCostos3;

                pIdCentroCostos4 = new SqlParameter();
                pIdCentroCostos4.ParameterName = "@IdCentroCostos4";
                pIdCentroCostos4.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos4.Value = objBE.IdCentroCostos4;

                pIdCentroCostos5 = new SqlParameter();
                pIdCentroCostos5.ParameterName = "@IdCentroCostos5";
                pIdCentroCostos5.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos5.Value = objBE.IdCentroCostos5;

                pIdMetodoPago = new SqlParameter();
                pIdMetodoPago.ParameterName = "@IdMetodoPago";
                pIdMetodoPago.SqlDbType = SqlDbType.Int;
                pIdMetodoPago.Value = objBE.IdMetodoPago;
                
                pMontoInicial = new SqlParameter();
                pMontoInicial.ParameterName = "@MontoInicial";
                pMontoInicial.SqlDbType = SqlDbType.VarChar;
                pMontoInicial.Size = 20;
                pMontoInicial.Value = objBE.MontoInicial;

                pMontoGastado = new SqlParameter();
                pMontoGastado.ParameterName = "@MontoGastado";
                pMontoGastado.SqlDbType = SqlDbType.VarChar;
                pMontoGastado.Size = 20;
                pMontoGastado.Value = objBE.MontoGastado;

                pMontoActual = new SqlParameter();
                pMontoActual.ParameterName = "@MontoActual";
                pMontoActual.SqlDbType = SqlDbType.VarChar;
                pMontoActual.Size = 20;
                pMontoActual.Value = objBE.MontoActual;

                pMoneda = new SqlParameter();
                pMoneda.ParameterName = "@Moneda";
                pMoneda.SqlDbType = SqlDbType.VarChar;
                pMoneda.Size = 3;
                pMoneda.Value = objBE.Moneda;

                pEsFacturable = new SqlParameter();
                pEsFacturable.ParameterName = "@EsFacturable";
                pEsFacturable.SqlDbType = SqlDbType.VarChar;
                pEsFacturable.Size = 3;
                pEsFacturable.Value = objBE.EsFacturable;

                pMomentoFacturable = new SqlParameter();
                pMomentoFacturable.ParameterName = "@MomentoFacturable";
                pMomentoFacturable.SqlDbType = SqlDbType.VarChar;
                pMomentoFacturable.Size = 3;
                pMomentoFacturable.Value = objBE.MomentoFacturable;

                pAsunto = new SqlParameter();
                pAsunto.ParameterName = "@Asunto";
                pAsunto.SqlDbType = SqlDbType.VarChar;
                pAsunto.Size = 100;
                pAsunto.Value = objBE.Asunto;

                pComentario = new SqlParameter();
                pComentario.ParameterName = "@Comentario";
                pComentario.SqlDbType = SqlDbType.VarChar;
                pComentario.Size = 1000;
                pComentario.Value = objBE.Comentario;

                pMotivoDetalle = new SqlParameter();
                pMotivoDetalle.ParameterName = "@MotivoDetalle";
                pMotivoDetalle.SqlDbType = SqlDbType.VarChar;
                pMotivoDetalle.Size = 5000;
                pMotivoDetalle.Value = objBE.MotivoDetalle;              

                pFechaSolicitud = new SqlParameter();
                pFechaSolicitud.ParameterName = "@FechaSolicitud";
                pFechaSolicitud.SqlDbType = SqlDbType.DateTime;
                pFechaSolicitud.Value = objBE.FechaSolicitud;

                pFechaContabilizacion = new SqlParameter();
                pFechaContabilizacion.ParameterName = "@FechaContabilizacion";
                pFechaContabilizacion.SqlDbType = SqlDbType.DateTime;
                pFechaContabilizacion.Value = objBE.FechaContabilizacion;

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

                sqlCmd.Parameters.Add(pIdDocumento);
                sqlCmd.Parameters.Add(pCodigoDocumento);
                sqlCmd.Parameters.Add(pIdEmpresa);
                sqlCmd.Parameters.Add(pIdArea);
                sqlCmd.Parameters.Add(pIdUsuarioCreador);
                sqlCmd.Parameters.Add(pIdUsuarioSolicitante);
                sqlCmd.Parameters.Add(pIdCentroCostos1);
                sqlCmd.Parameters.Add(pIdCentroCostos2);
                sqlCmd.Parameters.Add(pIdCentroCostos3);
                sqlCmd.Parameters.Add(pIdCentroCostos4);
                sqlCmd.Parameters.Add(pIdCentroCostos5);
                sqlCmd.Parameters.Add(pIdMetodoPago);
                sqlCmd.Parameters.Add(pMontoInicial);
                sqlCmd.Parameters.Add(pMontoGastado);
                sqlCmd.Parameters.Add(pMontoActual);
                sqlCmd.Parameters.Add(pMoneda);
                sqlCmd.Parameters.Add(pEsFacturable);
                sqlCmd.Parameters.Add(pMomentoFacturable);
                sqlCmd.Parameters.Add(pAsunto);
                sqlCmd.Parameters.Add(pComentario);
                sqlCmd.Parameters.Add(pMotivoDetalle);
                sqlCmd.Parameters.Add(pFechaSolicitud);
                sqlCmd.Parameters.Add(pFechaContabilizacion);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdDocumento.Value);

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();

                return Id;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        // Modificar CajaChica
        public void ModificarDocumento(DocumentBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter prmIdDocumento;
            SqlParameter pCodigoDocumento;
            SqlParameter pIdEmpresa;
            SqlParameter pIdArea;
            SqlParameter pIdUsuarioCreador;
            SqlParameter pIdUsuarioSolicitante;
            SqlParameter pIdCentroCostos1;
            SqlParameter pIdCentroCostos2;
            SqlParameter pIdCentroCostos3;
            SqlParameter pIdCentroCostos4;
            SqlParameter pIdCentroCostos5;
            SqlParameter pIdMetodoPago;
            SqlParameter pMontoInicial;
            SqlParameter pMontoGastado;
            SqlParameter pMontoActual;
            SqlParameter pMoneda;
            SqlParameter pEsFacturable;
            SqlParameter pMomentoFacturable;
            SqlParameter pAsunto;
            SqlParameter pComentario;
            SqlParameter pMotivoDetalle;
            SqlParameter pFechaSolicitud;
            SqlParameter pFechaContabilizacion;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_CajaChicaModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                prmIdDocumento = new SqlParameter();
                prmIdDocumento.ParameterName = "@IdCajaChica";
                prmIdDocumento.SqlDbType = SqlDbType.Int;
                prmIdDocumento.Value = objBE.IdDocumento;

                pCodigoDocumento = new SqlParameter();
                pCodigoDocumento.ParameterName = "@CodigoCajaChica";
                pCodigoDocumento.SqlDbType = SqlDbType.VarChar;
                pCodigoDocumento.Size = 100;
                pCodigoDocumento.Value = objBE.CodigoDocumento;

                pIdEmpresa = new SqlParameter();
                pIdEmpresa.ParameterName = "@IdEmpresa";
                pIdEmpresa.SqlDbType = SqlDbType.Int;
                pIdEmpresa.Value = objBE.IdEmpresa;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdArea";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = objBE.IdArea;

                pIdUsuarioCreador = new SqlParameter();
                pIdUsuarioCreador.ParameterName = "@IdUsuarioCreador";
                pIdUsuarioCreador.SqlDbType = SqlDbType.Int;
                pIdUsuarioCreador.Value = objBE.IdUsuarioCreador;

                pIdUsuarioSolicitante = new SqlParameter();
                pIdUsuarioSolicitante.ParameterName = "@IdUsuarioSolicitante";
                pIdUsuarioSolicitante.SqlDbType = SqlDbType.Int;
                pIdUsuarioSolicitante.Value = objBE.IdUsuarioSolicitante;

                pIdCentroCostos1 = new SqlParameter();
                pIdCentroCostos1.ParameterName = "@IdCentroCostos1";
                pIdCentroCostos1.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos1.Value = objBE.IdCentroCostos1;

                pIdCentroCostos2 = new SqlParameter();
                pIdCentroCostos2.ParameterName = "@IdCentroCostos2";
                pIdCentroCostos2.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos2.Value = objBE.IdCentroCostos2;

                pIdCentroCostos3 = new SqlParameter();
                pIdCentroCostos3.ParameterName = "@IdCentroCostos3";
                pIdCentroCostos3.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos3.Value = objBE.IdCentroCostos3;

                pIdCentroCostos4 = new SqlParameter();
                pIdCentroCostos4.ParameterName = "@IdCentroCostos4";
                pIdCentroCostos4.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos4.Value = objBE.IdCentroCostos4;

                pIdCentroCostos5 = new SqlParameter();
                pIdCentroCostos5.ParameterName = "@IdCentroCostos5";
                pIdCentroCostos5.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos5.Value = objBE.IdCentroCostos5;

                pIdMetodoPago = new SqlParameter();
                pIdMetodoPago.ParameterName = "@IdMetodoPago";
                pIdMetodoPago.SqlDbType = SqlDbType.Int;
                pIdMetodoPago.Value = objBE.IdMetodoPago;

                pMontoInicial = new SqlParameter();
                pMontoInicial.ParameterName = "@MontoInicial";
                pMontoInicial.SqlDbType = SqlDbType.VarChar;
                pMontoInicial.Size = 20;
                pMontoInicial.Value = objBE.MontoInicial;

                pMontoGastado = new SqlParameter();
                pMontoGastado.ParameterName = "@MontoGastado";
                pMontoGastado.SqlDbType = SqlDbType.VarChar;
                pMontoGastado.Size = 20;
                pMontoGastado.Value = objBE.MontoGastado;

                pMontoActual = new SqlParameter();
                pMontoActual.ParameterName = "@MontoActual";
                pMontoActual.SqlDbType = SqlDbType.VarChar;
                pMontoActual.Size = 20;
                pMontoActual.Value = objBE.MontoActual;

                pMoneda = new SqlParameter();
                pMoneda.ParameterName = "@Moneda";
                pMoneda.SqlDbType = SqlDbType.VarChar;
                pMoneda.Size = 3;
                pMoneda.Value = objBE.Moneda;

                pEsFacturable = new SqlParameter();
                pEsFacturable.ParameterName = "@EsFacturable";
                pEsFacturable.SqlDbType = SqlDbType.VarChar;
                pEsFacturable.Size = 3;
                pEsFacturable.Value = objBE.EsFacturable;

                pMomentoFacturable = new SqlParameter();
                pMomentoFacturable.ParameterName = "@MomentoFacturable";
                pMomentoFacturable.SqlDbType = SqlDbType.VarChar;
                pMomentoFacturable.Size = 3;
                pMomentoFacturable.Value = objBE.MomentoFacturable; 
                
                pAsunto = new SqlParameter();
                pAsunto.ParameterName = "@Asunto";
                pAsunto.SqlDbType = SqlDbType.VarChar;
                pAsunto.Size = 100;
                pAsunto.Value = objBE.Asunto;

                pComentario = new SqlParameter();
                pComentario.ParameterName = "@Comentario";
                pComentario.SqlDbType = SqlDbType.VarChar;
                pComentario.Size = 1000;
                pComentario.Value = objBE.Comentario;

                pMotivoDetalle = new SqlParameter();
                pMotivoDetalle.ParameterName = "@MotivoDetalle";
                pMotivoDetalle.SqlDbType = SqlDbType.VarChar;
                pMotivoDetalle.Size = 5000;
                pMotivoDetalle.Value = objBE.MotivoDetalle;    

                pFechaSolicitud = new SqlParameter();
                pFechaSolicitud.ParameterName = "@FechaSolicitud";
                pFechaSolicitud.SqlDbType = SqlDbType.DateTime;
                pFechaSolicitud.Value = objBE.FechaSolicitud;

                pFechaContabilizacion = new SqlParameter();
                pFechaContabilizacion.ParameterName = "@FechaContabilizacion";
                pFechaContabilizacion.SqlDbType = SqlDbType.DateTime;
                pFechaContabilizacion.Value = objBE.FechaContabilizacion;

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

                sqlCmd.Parameters.Add(prmIdDocumento);
                sqlCmd.Parameters.Add(pCodigoDocumento);
                sqlCmd.Parameters.Add(pIdEmpresa);
                sqlCmd.Parameters.Add(pIdArea);
                sqlCmd.Parameters.Add(pIdUsuarioCreador);
                sqlCmd.Parameters.Add(pIdUsuarioSolicitante);
                sqlCmd.Parameters.Add(pIdCentroCostos1);
                sqlCmd.Parameters.Add(pIdCentroCostos2);
                sqlCmd.Parameters.Add(pIdCentroCostos3);
                sqlCmd.Parameters.Add(pIdCentroCostos4);
                sqlCmd.Parameters.Add(pIdCentroCostos5);
                sqlCmd.Parameters.Add(pIdMetodoPago);
                sqlCmd.Parameters.Add(pMontoInicial);
                sqlCmd.Parameters.Add(pMontoGastado);
                sqlCmd.Parameters.Add(pMontoActual);
                sqlCmd.Parameters.Add(pMoneda);
                sqlCmd.Parameters.Add(pEsFacturable);
                sqlCmd.Parameters.Add(pMomentoFacturable);
                sqlCmd.Parameters.Add(pAsunto);
                sqlCmd.Parameters.Add(pComentario);
                sqlCmd.Parameters.Add(pMotivoDetalle);
                sqlCmd.Parameters.Add(pFechaSolicitud);
                sqlCmd.Parameters.Add(pFechaContabilizacion);
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
                throw;
            }
        }
    }
}
