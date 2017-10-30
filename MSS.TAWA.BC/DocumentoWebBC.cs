using MSS.TAWA.BE;
using MSS.TAWA.DA;
using MSS.TAWA.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace MSS.TAWA.BC
{
    public class DocumentoWebBC
    {
        public List<DocumentoWebBE> GetListRendicionesPendientesReembolso(Int32 idUsuario, String moneda)
        {
            List<DocumentoWeb> _list = new DocumentoWebDA().GetListRendicionesPendientesReembolso(idUsuario, moneda);
            List<DocumentoWebBE> list = new List<DocumentoWebBE>();


            DocumentoWebBE documentoParaSeleccionar = new DocumentoWebBE(TipoDocumentoWeb.EntregaRendir)
            {
                IdDocumentoWeb = -1,
                CodigoDocumento = "Seleccionar",
            };

            list.Add(documentoParaSeleccionar);

            foreach (var doc in _list)
            {
                list.Add(GetDocumentoWeb(doc.IdDocumentoWeb));
            }
            return list;
        }

        public List<DocumentoWebBE> GetList(Int32 idUsuario, TipoDocumentoWeb tipoDocumentoWeb)
        {
            try
            {
                List<DocumentoWeb> _list = new DocumentoWebDA().GetListDocumentoWeb(idUsuario, tipoDocumentoWeb);
                List<DocumentoWebBE> list = new List<DocumentoWebBE>();

                foreach (var doc in _list)
                {
                    list.Add(GetDocumentoWeb(doc.IdDocumentoWeb));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DocumentoWebBE GetDocumentoWeb(int IdDocumento)
        {
            try
            {
                DocumentoWeb item = new DocumentoWebDA().GetDocumentoWeb(IdDocumento);
                DocumentoWebBE documentoBE = new DocumentoWebBE((TipoDocumentoWeb)item.TipoDocumentoWeb);
                documentoBE.IdDocumentoWeb = item.IdDocumentoWeb;
                documentoBE.Asunto = item.Asunto;
                documentoBE.CodigoDocumento = item.Codigo;
                documentoBE.Comentario = item.Comentario;
                documentoBE.CreateDate = item.CreateDate ?? DateTime.Now;
                documentoBE.Estado = item.EstadoDocumento.ToString();
                documentoBE.FechaContabilizacion = item.FechaContabilizacion ?? DateTime.Now;
                documentoBE.FechaSolicitud = item.FechaSolicitud;
                documentoBE.IdCentroCostos1 = item.SAPCodigoCentroCostos1;
                documentoBE.IdCentroCostos2 = item.SAPCodigoCentroCostos2;
                documentoBE.IdCentroCostos3 = item.SAPCodigoCentroCostos3;
                documentoBE.IdCentroCostos4 = item.SAPCodigoCentroCostos4;
                documentoBE.IdCentroCostos5 = item.SAPCodigoCentroCostos5;
                documentoBE.IdEmpresa = item.IdEmpresa ?? 0;
                documentoBE.IdDocumentoWebRendicionReferencia = item.IdDocumentoWebRendicionReferencia;
                documentoBE.IdMetodoPago = item.IdMetodoPago;
                documentoBE.IdUsuarioCreador = item.IdUsuarioCreacion;
                documentoBE.IdUsuarioSolicitante = item.IdUsuarioSolicitante;
                documentoBE.Moneda = item.IdMoneda;
                documentoBE.MontoInicial = item.MontoInicial;
                documentoBE.MontoGastado = item.DocumentoWebRendicion.Where(x => x.NumeroRendicion == item.NumeroRendicion).ToList().Sum(x => x.MontoTotal);
                documentoBE.MontoActual = documentoBE.MontoInicial - documentoBE.MontoGastado;
                documentoBE.MotivoDetalle = item.MotivoDetalle;
                documentoBE.UpdateDate = item.UpdateDate ?? DateTime.Now;
                documentoBE.UserCreate = item.IdUsuarioCreacion.ToString();
                documentoBE.UserUpdate = item.IdUsuarioModificacion.ToString();
                return documentoBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddUpdateDocumento(DocumentoWebBE objBE)
        {
            new DocumentoWebDA().AddUpdateDocumentoWeb(objBE);
        }

        public void AprobarDocumento(CambioEstadoBE cambioEstadoBE)
        {
            new DocumentoWebDA().AprobarDocumento(cambioEstadoBE);
        }

        public void RechazarDocumento(CambioEstadoBE cambioEstadoBE)
        {
            new DocumentoWebDA().RechazarDocumento(cambioEstadoBE);
        }

        public void AprobarYLiquidarDocumento(CambioEstadoBE cambioEstadoBEo)
        {
            DocumentoWeb documentoWeb = new DocumentoWebDA().GetDocumentoWeb(cambioEstadoBEo.IdDocumentoWeb);
            if (documentoWeb.DocumentoWebRendicion.Where(x => x.NumeroRendicion == documentoWeb.NumeroRendicion).ToList().Sum(x => x.MontoTotal) < documentoWeb.MontoInicial)
                throw new Exception("No se puede liquidar. El documento aún cuenta con saldo.");

            new DocumentoWebDA().AprobarYLiquidarDocumento(cambioEstadoBEo);
        }

        public void EnviarRendicion(Int32 idDocumentoWeb)
        {
            new DocumentoWebDA().EnviarRendicion(idDocumentoWeb);
        }


        #region DocumentoRendicion

        public List<DocumentoWebRendicionBE> GetList(Int32 idDocumentoWeb, Boolean ListarSoloEstadoGuardado)
        {
            try
            {
                List<DocumentoWebRendicion> _list = new DocumentoWebDA().GetListDocumentoWebRendicion(idDocumentoWeb);
                if (ListarSoloEstadoGuardado)
                    _list = _list.Where(x => x.EstadoRendicion == (Int32)EstadoDocumentoRendicion.Guardado).ToList();

                List<DocumentoWebRendicionBE> list = new List<DocumentoWebRendicionBE>();

                foreach (var doc in _list)
                {
                    list.Add(GetDocumentoWebRendicion(doc.IdDocumentoWebRendicion, idDocumentoWeb));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DocumentoWebRendicionBE GetDocumentoWebRendicion(Int32? IdDocumentoWebRendicion, Int32? idDocumentoWeb = null)
        {
            try
            {
                DocumentoWebRendicionBE documentoBE = new DocumentoWebRendicionBE();
                DocumentoWebRendicion item = new DocumentoWebDA().GetDocumentoWebRendicion(IdDocumentoWebRendicion);

                if (item != null)
                {
                    documentoBE.IdDocumentoWeb = item.IdDocumentoWeb;
                    documentoBE.IdDocumentoWebRendicion = item.IdDocumentoWebRendicion;
                    documentoBE.CorrelativoDoc = item.CorrelativoDoc;
                    documentoBE.CreateDate = item.CreateDate;
                    documentoBE.Estado = item.EstadoRendicion.ToString();
                    documentoBE.FechaDoc = item.FechaDoc;
                    documentoBE.IdCentroCostos1 = item.SAPCodigoCentroCostos1;
                    documentoBE.IdCentroCostos2 = item.SAPCodigoCentroCostos2;
                    documentoBE.IdCentroCostos3 = item.SAPCodigoCentroCostos3;
                    documentoBE.IdCentroCostos4 = item.SAPCodigoCentroCostos4;
                    documentoBE.IdCentroCostos5 = item.SAPCodigoCentroCostos5;
                    documentoBE.IdConcepto = item.SAPCodigoConcepto;
                    documentoBE.IdDocumentoWeb = item.IdDocumentoWeb;
                    documentoBE.IdDocumentoWebRendicion = item.IdDocumentoWebRendicion;
                    documentoBE.IdMonedaDoc = item.IdMonedaDoc;
                    documentoBE.IdMonedaOriginal = item.DocumentoWeb.IdMoneda;
                    documentoBE.IdProveedor = item.IdProveedor ?? 0;
                    documentoBE.MontoAfecto = item.MontoAfecto;
                    documentoBE.MontoDoc = item.MontoDoc;
                    documentoBE.MontoIGV = item.MontoIGV;
                    documentoBE.MontoNoAfecto = item.MontoNoAfecto;
                    documentoBE.MontoTotal = item.MontoTotal;
                    documentoBE.CodigoPartidaPresupuestal = item.SAPCodigoPartidaPresupuestal;
                    documentoBE.Rendicion = item.NumeroRendicion;
                    documentoBE.SerieDoc = item.SerieDoc;
                    documentoBE.TasaCambio = item.MontoTasaCambio;
                    documentoBE.TipoDoc = item.IdTipoDocSunat.ToString();
                    documentoBE.UpdateDate = DateTime.Now; //TODO:
                    documentoBE.UserCreate = item.IdUsuarioCreacion;
                    documentoBE.UserUpdate = item.IdUsuarioModificacion;
                }
                else
                {
                    DocumentoWeb documentoWeb = new DocumentoWebDA().GetDocumentoWeb(idDocumentoWeb);
                    if (documentoWeb == null)
                        throw new Exception("No se encontró el documento principal.");
                    else
                    {
                        documentoBE.IdDocumentoWeb = documentoWeb.IdDocumentoWeb;
                        documentoBE.IdMonedaOriginal = documentoWeb.IdMoneda;
                    }
                }

                return documentoBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddUpdateDocumentoWebRendicion(DocumentoWebRendicionBE objBE)
        {
            using (TransactionScope trx = new TransactionScope())
            {
                new DocumentoWebDA().AddUpdateDocumentoRendicion(objBE);
                DocumentoWeb documentoWeb = new DocumentoWebDA().GetDocumentoWeb(objBE.IdDocumentoWeb);

                //Valida si rendición excede el monto inicial del documento principal.
                if ((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb == TipoDocumentoWeb.EntregaRendir
                    && objBE.TipoDoc == ((Int32)TipoDocumentoSunat.Devolucion).ToString()
                    || (TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb == TipoDocumentoWeb.CajaChica
                    || (TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb == TipoDocumentoWeb.Reembolso
                    )
                {
                    Decimal montoGastadoTotal = documentoWeb.DocumentoWebRendicion.Where(x => x.NumeroRendicion == documentoWeb.NumeroRendicion).ToList().Sum(x => x.MontoTotal);
                    if (montoGastadoTotal > documentoWeb.MontoInicial)
                        throw new Exception("El monto que desea agregar supera al monto inicial del documento");
                }
                trx.Complete();
            }
        }

        public void EliminarDocumentoDetalle(int idDocumentoDetalle)
        {
            new DocumentoWebDA().DeleteDocumentoRendicion(idDocumentoDetalle);
        }

        #endregion
    }
}
