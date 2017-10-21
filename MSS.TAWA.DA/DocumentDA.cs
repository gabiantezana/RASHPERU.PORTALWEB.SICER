using MSS.TAWA.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MSS.TAWA.DA
{
    public class DocumentDA
    {
        TipoDocumentoWeb _TipoDocumentoWeb { get; set; }

        public DocumentDA(TipoDocumentoWeb tipoDocumentoWeb)
        {
            _TipoDocumentoWeb = tipoDocumentoWeb;
        }

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
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaListar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirListar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoListar";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
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
                    switch (_TipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));
                            objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoCajaChica"));
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendir"));
                            objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoEntregaRendir"));
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolso"));
                            objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoReembolso"));
                            break;
                        default:
                            throw new NotImplementedException();
                    }

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
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaObtener";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirObtener";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoObtener";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdDocumento = new SqlParameter();
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pIdDocumento.ParameterName = "@IdCajaChica";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pIdDocumento.ParameterName = "@IdEntregaRendir";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pIdDocumento.ParameterName = "@IdReembolso";
                        break;
                    default:
                        throw new NotImplementedException();
                }
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
                    switch (_TipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));
                            objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoCajaChica"));
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendir"));
                            objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoEntregaRendir"));
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            objDocumentoBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolso"));
                            objDocumentoBE.CodigoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("CodigoReembolso"));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
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
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaInsertar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirInsertar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoInsertar";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdDocumento = new SqlParameter();
                pIdDocumento.Direction = ParameterDirection.ReturnValue;
                pIdDocumento.SqlDbType = SqlDbType.Int;

                pCodigoDocumento = new SqlParameter();
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pCodigoDocumento.ParameterName = "@CodigoCajaChica";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pCodigoDocumento.ParameterName = "@CodigoEntregaRendir";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pCodigoDocumento.ParameterName = "@CodigoReembolso";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                pCodigoDocumento.SqlDbType = SqlDbType.VarChar;
                pCodigoDocumento.Size = 100;
                pCodigoDocumento.Value = "1";//objBE.CodigoDocumento;

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
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaModificar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirModificar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoModificar";
                        break;
                    default:
                        throw new NotImplementedException();
                }


                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                prmIdDocumento = new SqlParameter();

                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        prmIdDocumento.ParameterName = "@IdCajaChica";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        prmIdDocumento.ParameterName = "@IdEntregaRendir";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        prmIdDocumento.ParameterName = "@IdReembolso";
                        break;
                    default:
                        break;
                }
                prmIdDocumento.SqlDbType = SqlDbType.Int;
                prmIdDocumento.Value = objBE.IdDocumento;

                pCodigoDocumento = new SqlParameter();
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pCodigoDocumento.ParameterName = "@CodigoCajaChica";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        prmIdDocumento.ParameterName = "@CodigoEntregaRendir";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        prmIdDocumento.ParameterName = "@CodigoReembolso";
                        break;
                    default:
                        break;
                }
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

        #region Detalle Documentos

        public List<DocumentDetailBE> ListarDocumentoDetalle(int Id, int Tipo, int Tipo2)
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
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaDocumentoListar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirDocumentoListar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoDocumentoListar";
                        break;
                    default:
                        throw new NotImplementedException();
                }
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

                List<DocumentDetailBE> lstDocumentosBE;
                DocumentDetailBE documentoBE;
                lstDocumentosBE = new List<DocumentDetailBE>();

                while (sqlDR.Read())
                {
                    documentoBE = new DocumentDetailBE();
                    SetDocumentoDetalleProperties(sqlDR, ref documentoBE);
                    lstDocumentosBE.Add(documentoBE);
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

        public DocumentDetailBE ObtenerDocumentoDetalle(int Id, int Tipo)
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
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaDocumentoObtener";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirDocumentoObtener";

                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoDocumentoObtener";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
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

                DocumentDetailBE documentBE;
                documentBE = null;

                while (sqlDR.Read())
                {
                    documentBE = new DocumentDetailBE();
                    SetDocumentoDetalleProperties(sqlDR, ref documentBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return documentBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarDocumentoDetalle(DocumentDetailBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdCajaChicaDocumento;
            SqlParameter pIdCajaChica;
            SqlParameter pIdProveedor;
            SqlParameter pIdConcepto;
            SqlParameter pIdCentroCostos1;
            SqlParameter pIdCentroCostos2;
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
            SqlParameter pPartidaPresupuestal;

            int Id;

            try
            {
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaDocumentoInsertar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirDocumentoInsertar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoDocumentoInsertar";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdCajaChicaDocumento = new SqlParameter();
                pIdCajaChicaDocumento.Direction = ParameterDirection.ReturnValue;
                pIdCajaChicaDocumento.SqlDbType = SqlDbType.Int;

                pIdCajaChica = new SqlParameter();
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pIdCajaChica.ParameterName = "@IdCajaChica";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pIdCajaChica.ParameterName = "@IdEntregaRendir";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pIdCajaChica.ParameterName = "@IdReembolso";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                pIdCajaChica.SqlDbType = SqlDbType.Int;
                pIdCajaChica.Value = objBE.IdDocumento;

                pIdProveedor = new SqlParameter();
                pIdProveedor.ParameterName = "@IdProveedor";
                pIdProveedor.SqlDbType = SqlDbType.Int;
                pIdProveedor.Value = objBE.IdProveedor;

                pIdConcepto = new SqlParameter();
                pIdConcepto.ParameterName = "@IdConcepto";
                pIdConcepto.SqlDbType = SqlDbType.NVarChar;
                pIdConcepto.Value = objBE.IdConcepto;

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

                pPartidaPresupuestal = new SqlParameter();
                pPartidaPresupuestal.ParameterName = "@PartidaPresupuestal";
                pPartidaPresupuestal.SqlDbType = SqlDbType.NVarChar;
                pPartidaPresupuestal.Value = objBE.PartidaPresupuestal;


                sqlCmd.Parameters.Add(pIdCajaChicaDocumento);
                sqlCmd.Parameters.Add(pIdCajaChica);
                sqlCmd.Parameters.Add(pIdProveedor);
                sqlCmd.Parameters.Add(pIdConcepto);
                sqlCmd.Parameters.Add(pIdCentroCostos1);
                sqlCmd.Parameters.Add(pIdCentroCostos2);
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
                sqlCmd.Parameters.Add(pPartidaPresupuestal);

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
                throw;
            }
        }

        public void ModificarDocumentoDetalle(DocumentDetailBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdCajaChicaDocumento;
            SqlParameter pIdCajaChica;
            SqlParameter pIdProveedor;
            SqlParameter pIdConcepto;
            SqlParameter pIdCentroCostos1;
            SqlParameter pIdCentroCostos2;
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
            SqlParameter pPartidaPresupuestal;

            try
            {
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaDocumentoModificar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirDocumentoModificar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoDocumentoModificar";
                        break;
                    default:
                        throw new NotImplementedException();
                }


                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdCajaChicaDocumento = new SqlParameter();
                pIdCajaChicaDocumento.ParameterName = "@IdCajaChicaDocumento";
                pIdCajaChicaDocumento.SqlDbType = SqlDbType.Int;
                pIdCajaChicaDocumento.Value = objBE.IdDocumentoDetalle;

                pIdCajaChica = new SqlParameter();
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pIdCajaChica.ParameterName = "@IdCajaChica";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pIdCajaChica.ParameterName = "@IdEntregaRendir";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pIdCajaChica.ParameterName = "@IdReembolso";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                pIdCajaChica.SqlDbType = SqlDbType.Int;
                pIdCajaChica.Value = objBE.IdDocumento;

                pIdProveedor = new SqlParameter();
                pIdProveedor.ParameterName = "@IdProveedor";
                pIdProveedor.SqlDbType = SqlDbType.Int;
                pIdProveedor.Value = objBE.IdProveedor;

                pIdConcepto = new SqlParameter();
                pIdConcepto.ParameterName = "@IdConcepto";
                pIdConcepto.SqlDbType = SqlDbType.NVarChar;
                pIdConcepto.Value = objBE.IdConcepto;

                pIdCentroCostos1 = new SqlParameter();
                pIdCentroCostos1.ParameterName = "@IdCentroCostos1";
                pIdCentroCostos1.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos1.Value = objBE.IdCentroCostos1;

                pIdCentroCostos2 = new SqlParameter();
                pIdCentroCostos2.ParameterName = "@IdCentroCostos2";
                pIdCentroCostos2.SqlDbType = SqlDbType.NVarChar;
                pIdCentroCostos2.Value = objBE.IdCentroCostos1;

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

                pPartidaPresupuestal = new SqlParameter();
                pPartidaPresupuestal.ParameterName = "@PartidaPresupuestal";
                pPartidaPresupuestal.SqlDbType = SqlDbType.NVarChar;
                pPartidaPresupuestal.Value = objBE.PartidaPresupuestal;


                sqlCmd.Parameters.Add(pIdCajaChicaDocumento);
                sqlCmd.Parameters.Add(pIdCajaChica);
                sqlCmd.Parameters.Add(pIdProveedor);
                sqlCmd.Parameters.Add(pIdConcepto);
                sqlCmd.Parameters.Add(pIdCentroCostos1);
                sqlCmd.Parameters.Add(pIdCentroCostos2);
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
                sqlCmd.Parameters.Add(pPartidaPresupuestal);

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

        public void EliminarDocumentoDetalle(int IdCajaChicaDocumento)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdCajaChicaDocumento;

            try
            {
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        strSP = "MSS_WEB_CajaChicaDocumentoEliminar";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        strSP = "MSS_WEB_EntregaRendirDocumentoEliminar";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        strSP = "MSS_WEB_ReembolsoDocumentoEliminar";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdCajaChicaDocumento = new SqlParameter();
                switch (_TipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pIdCajaChicaDocumento.ParameterName = "@IdCajaChicaDocumento";
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pIdCajaChicaDocumento.ParameterName = "@IdEntregaRendirDocumento";
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pIdCajaChicaDocumento.ParameterName = "@IdReembolsoDocumento";
                        break;
                    default:
                        throw new NotImplementedException();
                }
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
                throw;
            }
        }

        private void SetDocumentoDetalleProperties(SqlDataReader sqlDR, ref DocumentDetailBE documentBE)
        {
            switch (_TipoDocumentoWeb)
            {
                case TipoDocumentoWeb.CajaChica:
                    documentBE.IdDocumentoDetalle = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChicaDocumento"));
                    documentBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCajaChica"));
                    break;
                case TipoDocumentoWeb.EntregaRendir:
                    documentBE.IdDocumentoDetalle = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendirDocumento"));
                    documentBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEntregaRendir"));
                    break;
                case TipoDocumentoWeb.Reembolso:
                    documentBE.IdDocumentoDetalle = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolsoDocumento"));
                    documentBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdReembolso"));
                    break;
                default:
                    break;
            }
            documentBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
            documentBE.IdConcepto = sqlDR.GetString(sqlDR.GetOrdinal("IdConcepto"));
            documentBE.IdCentroCostos1 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos1"));
            documentBE.IdCentroCostos2 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos2"));
            documentBE.IdCentroCostos3 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos3"));
            documentBE.IdCentroCostos4 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos4"));
            documentBE.IdCentroCostos5 = sqlDR.GetString(sqlDR.GetOrdinal("IdCentroCostos5"));
            documentBE.TipoDoc = sqlDR.GetString(sqlDR.GetOrdinal("TipoDoc"));
            documentBE.SerieDoc = sqlDR.GetString(sqlDR.GetOrdinal("SerieDoc"));
            documentBE.CorrelativoDoc = sqlDR.GetString(sqlDR.GetOrdinal("CorrelativoDoc"));
            documentBE.FechaDoc = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaDoc"));
            documentBE.IdMonedaDoc = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaDoc"));
            documentBE.MontoDoc = sqlDR.GetString(sqlDR.GetOrdinal("MontoDoc"));
            documentBE.TasaCambio = sqlDR.GetString(sqlDR.GetOrdinal("TasaCambio"));
            documentBE.IdMonedaOriginal = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMonedaOriginal"));
            documentBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
            documentBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
            documentBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
            documentBE.MontoTotal = sqlDR.GetString(sqlDR.GetOrdinal("MontoTotal"));
            documentBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
            documentBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
            documentBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
            documentBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
            documentBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
            documentBE.PartidaPresupuestal = sqlDR.GetString(sqlDR.GetOrdinal("PartidaPresupuestal"));
        }

        #endregion
    }
}
