using MSS.TAWA.BE;
using MSS.TAWA.MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace MSS.TAWA.DA
{
    public class DocumentoWebDA
    {
        public List<DocumentoWeb> GetListRendicionesPendientesReembolso(Int32 idUsuario, String moneda)
        {
            var dataContext = new SICER_WEBEntities1();
            List<DocumentoWeb> DocumentosWebQueSePuedenReembolsar = new List<DocumentoWeb>();

            List<DocumentoWeb> documentosDeUsuario = GetListDocumentoWeb(idUsuario, TipoDocumentoWeb.EntregaRendir).Where(x => x.EstadoDocumento != (Int32)EstadoDocumento.Liquidado).ToList();
            foreach (DocumentoWeb docmento in documentosDeUsuario)
            {
                var rendicionesASumar = docmento.DocumentoWebRendicion.ToList().Where(x => x.EstadoRendicion == (Int32)EstadoDocumentoRendicion.Guardado
                                                                        || x.EstadoRendicion == (Int32)EstadoDocumentoRendicion.Rendido
                                                                        && x.NumeroRendicion == docmento.NumeroRendicion
                                                                        && x.IdMonedaDoc == Convert.ToInt32(moneda)
                                                                        ).ToList();

                decimal totalMontoDocumento = rendicionesASumar.Sum(x => x.MontoTotal);
                if (totalMontoDocumento > docmento.MontoInicial)
                    DocumentosWebQueSePuedenReembolsar.Add(docmento);
            }
            return DocumentosWebQueSePuedenReembolsar;
        }

        public List<DocumentoWeb> GetListDocumentoWeb(Int32 IdUsuario, TipoDocumentoWeb tipoDocumentoWeb)
        {
            SICER_WEBEntities1 dataContext = new SICER_WEBEntities1();
            Usuario usuario = dataContext.Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();

            if (usuario.IdPerfilUsuario == null)
                throw new Exception("El usuario no tiene ningún perfil asignado. No se pueden listar los documentos.");


            Boolean esAdministrador = false;
            Boolean esCreador = false;
            Boolean esAprobador = false;
            Boolean esContador = false;
            Boolean esSistemas = false;
            ;
            switch ((PerfilUsuario)usuario.IdPerfilUsuario)
            {
                case PerfilUsuario.AdministradorWeb:
                    esAdministrador = true;
                    break;
                case PerfilUsuario.ContabilidadyCreador_CC_ER_RE:
                    esCreador = true;
                    esContador = true;
                    break;
                case PerfilUsuario.AprobadoryCreador_CC_ER_RE:
                    esCreador = true;
                    esAprobador = true;
                    break;
                case PerfilUsuario.Creador_CC_ER_RE:
                    esCreador = true;
                    break;
                case PerfilUsuario.Contabilidad:
                    esContador = true;
                    break;
                case PerfilUsuario.Aprobador_CC_ER_RE:
                    esAprobador = true;
                    break;
                case PerfilUsuario.Creador_CC:
                    esCreador = true;
                    break;
                case PerfilUsuario.Creador_ER:
                    esCreador = true;
                    break;
                case PerfilUsuario.Creador_RE:
                    esCreador = true;
                    break;
                case PerfilUsuario.Creador_ER_RE:
                    esCreador = true;
                    break;
                case PerfilUsuario.ContabilidadyCreador_ER_RE:
                    esCreador = true;
                    esContador = true;
                    break;
                case PerfilUsuario.AprobadoryCreador_ER_RE:
                    esCreador = true;
                    esAprobador = true;
                    break;
                case PerfilUsuario.Sistemas:
                    esSistemas = true;
                    break;
                default:
                    throw new NotImplementedException();
            }

            List<DocumentoWeb> documentosCreadosPorUsuario = new List<DocumentoWeb>();
            List<DocumentoWeb> pendientesAprobarNivelPorUsuario = new List<DocumentoWeb>();
            List<DocumentoWeb> pendientesPorAprobarContabilidad = new List<DocumentoWeb>();
            List<DocumentoWeb> documentosDeSistemas = new List<DocumentoWeb>();
            List<DocumentoWeb> documentosDeAdministradores = new List<DocumentoWeb>();

            if (esSistemas)
                documentosDeSistemas = dataContext.DocumentoWeb.ToList();

            if (esAdministrador)
                documentosDeAdministradores = dataContext.DocumentoWeb.ToList();

            if (esCreador)
                documentosCreadosPorUsuario = dataContext.DocumentoWeb.Where(x => x.IdUsuarioSolicitante == IdUsuario).ToList();

            if (esAprobador)
            {
                switch (tipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pendientesAprobarNivelPorUsuario = dataContext.DocumentoWeb.Where(x =>
                                     (x.Usuario.IdUsuarioCC1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel1)
                                   || (x.Usuario.IdUsuarioCC2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel2)
                                   || (x.Usuario.IdUsuarioCC3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel3)).ToList();
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pendientesAprobarNivelPorUsuario = dataContext.DocumentoWeb.Where(x =>
                                    (x.Usuario.IdUsuarioER1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel1)
                                  || (x.Usuario.IdUsuarioER2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel2)
                                  || (x.Usuario.IdUsuarioER3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel3)).ToList();
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pendientesAprobarNivelPorUsuario = dataContext.DocumentoWeb.Where(x =>
                                    (x.Usuario.IdUsuarioRE1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel1)
                                  || (x.Usuario.IdUsuarioRE2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel2)
                                  || (x.Usuario.IdUsuarioRE3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento == EstadoDocumento.PorAprobarNivel3)).ToList();
                        break;
                    default:
                        throw new NotImplementedException();
                }

            }

            if (esContador)
                pendientesPorAprobarContabilidad = dataContext.DocumentoWeb.Where(x => x.EstadoDocumento == (Int32)EstadoDocumento.RendirPorAprobarContabilidad).ToList();


            List<DocumentoWeb> listDocumentoWeb = new List<DocumentoWeb>();
            listDocumentoWeb.AddRange(documentosDeSistemas);
            listDocumentoWeb.AddRange(documentosDeAdministradores);
            listDocumentoWeb.AddRange(documentosCreadosPorUsuario);
            listDocumentoWeb.AddRange(pendientesAprobarNivelPorUsuario);
            listDocumentoWeb.AddRange(pendientesPorAprobarContabilidad);

            listDocumentoWeb = listDocumentoWeb.Where(x => x.TipoDocumentoWeb == (Int32)tipoDocumentoWeb).Distinct().ToList();

            return listDocumentoWeb?.ToList() ?? new List<DocumentoWeb>();
        }

        #region DocumentoWeb

        public List<DocumentoWeb> GetListDocumentosPendientesPorUsuario(Int32 IdUsuario, TipoDocumentoWeb tipoDocumentoWeb)
        {
            List<EstadoDocumento> listadoEstadosNoPendientes = new List<EstadoDocumento>
            {
                EstadoDocumento.Aprobado,
                EstadoDocumento.Liquidado,
                EstadoDocumento.Rechazado,
                EstadoDocumento.RendirAprobado,
            };
            var list = GetListDocumentoWeb(IdUsuario, tipoDocumentoWeb).Where(x => !listadoEstadosNoPendientes.Contains((EstadoDocumento)x.EstadoDocumento));
            return list.ToList();
        }

        public DocumentoWeb GetDocumentoWeb(Int32? idDocumentoWeb)
        {
            var item = new SICER_WEBEntities1().DocumentoWeb.Find(idDocumentoWeb);
            return item;
        }

        public void AddUpdateDocumentoWeb(DocumentoWebBE documentoWebBE)
        {
            var dataContext = new SICER_WEBEntities1();
            DocumentoWeb documentoWeb;
            Boolean isUpdate = dataContext.DocumentoWeb.Find(documentoWebBE.IdDocumentoWeb) == null ? false : true;

            if (!isUpdate)
            {
                documentoWeb = new DocumentoWeb();
                documentoWeb.Codigo = GenerateDocumentCode((TipoDocumentoWeb)documentoWebBE.TipoDocumentoWeb);
                documentoWeb.EstadoDocumento = (Int32)EstadoDocumento.PorAprobarNivel1;
                documentoWeb.NumeroRendicion = 1;
                documentoWeb.CreateDate = DateTime.Now;
                documentoWeb.IdUsuarioCreacion = Convert.ToInt32(documentoWebBE.IdUsuarioCreador);
                documentoWeb.IdUsuarioSolicitante = documentoWebBE.IdUsuarioSolicitante;
                documentoWeb.IdUsuarioModificacion = documentoWeb.IdUsuarioCreacion;
                documentoWeb.TipoDocumentoWeb = (Int32)documentoWebBE.TipoDocumentoWeb;
            }
            else
            {
                documentoWeb = dataContext.DocumentoWeb.Find(documentoWebBE.IdDocumentoWeb);
            }

            documentoWeb.Asunto = documentoWebBE.Asunto;
            documentoWeb.Comentario = documentoWebBE.Comentario;
            documentoWeb.IdEmpresa = documentoWebBE.IdEmpresa;
            documentoWeb.IdDocumentoWebRendicionReferencia = documentoWebBE.IdDocumentoWebRendicionReferencia;
            documentoWeb.IdMoneda = documentoWebBE.Moneda;
            documentoWeb.IdUsuarioSolicitante = documentoWebBE.IdUsuarioSolicitante;
            documentoWeb.MontoInicial = documentoWebBE.MontoInicial;
            documentoWeb.MotivoDetalle = documentoWebBE.MotivoDetalle;
            documentoWeb.SAPCodigoCentroCostos1 = documentoWebBE.IdCentroCostos1;
            documentoWeb.SAPCodigoCentroCostos2 = documentoWebBE.IdCentroCostos2;
            documentoWeb.SAPCodigoCentroCostos3 = documentoWebBE.IdCentroCostos3;
            documentoWeb.SAPCodigoCentroCostos4 = documentoWebBE.IdCentroCostos4;
            documentoWeb.SAPCodigoCentroCostos5 = documentoWebBE.IdCentroCostos5;
            documentoWeb.IdMetodoPago = documentoWebBE.IdMetodoPago;
            documentoWeb.FechaContabilizacion = DateTime.Now;
            documentoWeb.FechaSolicitud = DateTime.Now;
            documentoWeb.Comentario = documentoWebBE.Comentario;

            if (isUpdate)
                dataContext.Entry(documentoWeb);
            else
                dataContext.DocumentoWeb.Add(documentoWeb);
            dataContext.SaveChanges();
        }

        public void AprobarDocumento(CambioEstadoBE cambioEstadoBE)
        {
            SICER_WEBEntities1 datacontext = new SICER_WEBEntities1();
            using (var trx = datacontext.Database.BeginTransaction())
            {
                try
                {
                    DocumentoWeb documentoWeb = datacontext.DocumentoWeb.Find(cambioEstadoBE.IdDocumentoWeb);

                    EstadoDocumento estadoActualDocumento = (EstadoDocumento)Convert.ToInt32(documentoWeb.EstadoDocumento);
                    EstadoDocumento nuevoEstado;

                    switch (estadoActualDocumento)
                    {
                        case EstadoDocumento.PorAprobarNivel1:
                            nuevoEstado = EstadoDocumento.PorAprobarNivel2;
                            break;
                        case EstadoDocumento.PorAprobarNivel2:
                            nuevoEstado = EstadoDocumento.Aprobado;
                            break;
                        case EstadoDocumento.PorAprobarNivel3:
                            throw new NotImplementedException();
                        case EstadoDocumento.Aprobado:
                            throw new Exception("El documento ya se encuentra aprobado.");
                        case EstadoDocumento.Rechazado:
                            nuevoEstado = EstadoDocumento.PorAprobarNivel1;
                            break;
                        case EstadoDocumento.RendirPorAprobarJefeArea:
                            nuevoEstado = EstadoDocumento.RendirPorAprobarContabilidad;
                            break;
                        case EstadoDocumento.RendirPorAprobarContabilidad:
                            nuevoEstado = EstadoDocumento.RendirAprobado;
                            break;
                        case EstadoDocumento.RendirAprobado:
                            throw new Exception("El documento ya se encuentra aprobado.");
                        case EstadoDocumento.Liquidado:
                            throw new Exception("El documento ya se encuentra aprobado y liquidado.");
                        default:
                            throw new NotImplementedException();
                    }

                    documentoWeb.EstadoDocumento = (Int32)nuevoEstado;
                    datacontext.Entry(documentoWeb);
                    datacontext.SaveChanges();

                    //GUARDA AUDITORÍA
                    DocumentoWebAuditoria documentoWebAuditoria = new DocumentoWebAuditoria();
                    documentoWebAuditoria.IdDocumentoWeb = cambioEstadoBE.IdDocumentoWeb;
                    documentoWebAuditoria.IdUsuario = cambioEstadoBE.IdUsuario;
                    documentoWebAuditoria.Comentario = cambioEstadoBE.Comentario;
                    documentoWebAuditoria.EstadoDocumento = documentoWeb.EstadoDocumento;
                    documentoWebAuditoria.Fecha = DateTime.Now;

                    datacontext.DocumentoWebAuditoria.Add(documentoWebAuditoria);
                    datacontext.SaveChanges();

                    //SI EL DOCUMENTO WEB ES APROBADO, SE MIGRA A LA BD INTERMEDIA.
                    if (nuevoEstado == EstadoDocumento.Aprobado)
                    {
                        if ((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb == TipoDocumentoWeb.Reembolso)
                        {
                            DocumentoWeb entregaRendirPorLiquidar = datacontext.DocumentoWeb.Find(documentoWeb.IdDocumentoWebRendicionReferencia);
                            entregaRendirPorLiquidar.EstadoDocumento = (Int32)EstadoDocumento.Liquidado;
                            datacontext.Entry(entregaRendirPorLiquidar);
                            datacontext.SaveChanges();
                        }

                        MigrateToInterDB(documentoWeb);
                    }

                    //SI LAS RENDICIONES SON APROBADAS, SE MIGRAN LAS RENDICIONES A LA BD INTERMEDIA.
                    else if (nuevoEstado == EstadoDocumento.RendirAprobado)
                    {
                        //MIGRA BUSINESS PARTNERS
                        MigrateBusinessPartner(documentoWeb.IdDocumentoWeb);

                        List<DocumentoWebRendicion> ListDocumentosGuardados = documentoWeb.DocumentoWebRendicion.Where(x => x.NumeroRendicion == documentoWeb.NumeroRendicion
                                                                              && x.EstadoRendicion == (Int32)EstadoDocumentoRendicion.Guardado).ToList();
                        foreach (DocumentoWebRendicion documentoWebRendicion in ListDocumentosGuardados)
                        {
                            documentoWebRendicion.EstadoRendicion = (Int32)EstadoDocumentoRendicion.Rendido;
                            datacontext.Entry(documentoWebRendicion);
                            datacontext.SaveChanges();

                            //MIGRA RENDICIONES
                            MigrateToInterDB(documentoWebRendicion);
                        }
                    }

                    trx.Commit();
                    trx.Dispose();
                }
                catch (Exception ex)
                {
                    trx.Rollback();
                    throw;
                }

            }
        }

        public void RechazarDocumento(CambioEstadoBE cambioEstadoBE)
        {
            SICER_WEBEntities1 datacontext = new SICER_WEBEntities1();
            DocumentoWeb documentoWeb = datacontext.DocumentoWeb.Find(cambioEstadoBE.IdDocumentoWeb);
            switch ((EstadoDocumento)documentoWeb.EstadoDocumento)
            {
                case EstadoDocumento.Aprobado:
                case EstadoDocumento.Liquidado:
                case EstadoDocumento.Rechazado:
                case EstadoDocumento.RendirAprobado:
                case EstadoDocumento.RendirPorAprobarContabilidad:
                case EstadoDocumento.RendirPorAprobarJefeArea:
                    throw new Exception("El documento no se puede rechazar. El estado actual del documento es: " + ((EstadoDocumento)documentoWeb.EstadoDocumento).ToString());
                default:
                    break;
            }

            documentoWeb.EstadoDocumento = (Int32)EstadoDocumento.Rechazado;
            var rendicionesARechazar = documentoWeb.DocumentoWebRendicion.Where(x => x.NumeroRendicion == documentoWeb.NumeroRendicion && x.EstadoRendicion == (Int32)EstadoDocumentoRendicion.Guardado).ToList();

            foreach (DocumentoWebRendicion documentoRendicion in rendicionesARechazar)
            {
                documentoRendicion.EstadoRendicion = (Int32)EstadoDocumentoRendicion.Rechazado;
                datacontext.Entry(documentoRendicion);
            }

            datacontext.Entry(documentoWeb);

            //GUARDA AUDITORÍA
            DocumentoWebAuditoria documentoWebAuditoria = new DocumentoWebAuditoria();
            documentoWebAuditoria.IdDocumentoWeb = cambioEstadoBE.IdDocumentoWeb;
            documentoWebAuditoria.IdUsuario = cambioEstadoBE.IdUsuario;
            documentoWebAuditoria.Comentario = cambioEstadoBE.Comentario;
            documentoWebAuditoria.EstadoDocumento = documentoWeb.EstadoDocumento;
            documentoWebAuditoria.Fecha = DateTime.Now;
            datacontext.DocumentoWebAuditoria.Add(documentoWebAuditoria);

            datacontext.SaveChanges();
        }

        public void EnviarRendicion(Int32 idDocumentoWeb)
        {
            var dataContext = new SICER_WEBEntities1();
            DocumentoWeb documentoWeb = dataContext.DocumentoWeb.Find(idDocumentoWeb);

            //Cambia estado a pendiente de rendición
            documentoWeb.EstadoDocumento = ((Int32)EstadoDocumento.RendirPorAprobarJefeArea);
            dataContext.Entry(documentoWeb);

            dataContext.SaveChanges();
        }

        public void MigrateBusinessPartner(Int32 idDocumentoWeb)
        {
            var list = new SICER_WEBEntities1().Proveedor.Where(x => x.IdProceso == idDocumentoWeb).ToList();

            foreach (var item in list)
            {
                MaestroTrabajadores maestroTrabajadores = new MaestroTrabajadores();
                var proveedor = new SICER_WEBEntities1().Proveedor.Where(x => x.CardCode == item.Documento && x.CardName == item.CardName).FirstOrDefault();
                if (proveedor == null)
                {

                    maestroTrabajadores.Code = "001";
                    maestroTrabajadores.CardCode = item.CardCode;
                    maestroTrabajadores.CardName = item.CardName;
                    maestroTrabajadores.CardType = "S";
                    maestroTrabajadores.LicTradNum = item.Documento;
                    maestroTrabajadores.GroupCode = 101;
                    maestroTrabajadores.U_BPP_BPTP = "TPJ";
                    maestroTrabajadores.U_BPP_BPTD = "6";
                    maestroTrabajadores.INT_Estado = "A";

                    var datacontext = new SICER_INT_SBOEntities();
                    datacontext.MaestroTrabajadores.Add(maestroTrabajadores);
                    datacontext.SaveChanges();
                }
            }

        }

        public void AprobarYLiquidarDocumento(CambioEstadoBE cambioEstadoBE)
        {
            SICER_WEBEntities1 dataContext = new SICER_WEBEntities1();
            AprobarDocumento(cambioEstadoBE);

            //Liquida documento
            DocumentoWeb documentoWeb = dataContext.DocumentoWeb.Find(cambioEstadoBE.IdDocumentoWeb);
            documentoWeb.EstadoDocumento = (Int32)EstadoDocumento.Liquidado;
            documentoWeb.NumeroRendicion += 1;
            dataContext.Entry(documentoWeb);

            //GUARDA AUDITORÍA
            DocumentoWebAuditoria documentoWebAuditoria = new DocumentoWebAuditoria();
            documentoWebAuditoria.IdDocumentoWeb = cambioEstadoBE.IdDocumentoWeb;
            documentoWebAuditoria.IdUsuario = cambioEstadoBE.IdUsuario;
            documentoWebAuditoria.Comentario = cambioEstadoBE.Comentario;
            documentoWebAuditoria.EstadoDocumento = documentoWeb.EstadoDocumento;
            documentoWebAuditoria.Fecha = DateTime.Now;
            dataContext.DocumentoWebAuditoria.Add(documentoWebAuditoria);


            dataContext.SaveChanges();
        }

        #endregion

        #region DocumentoWebRendicion

        public List<DocumentoWebRendicion> GetListDocumentoWebRendicion(Int32 IdDocumentoWeb)
        {
            var list = new SICER_WEBEntities1().DocumentoWebRendicion.Where(x => x.IdDocumentoWeb == IdDocumentoWeb && x.DocumentoWeb.NumeroRendicion == x.NumeroRendicion).ToList();
            return list;
        }

        public DocumentoWebRendicion GetDocumentoWebRendicion(Int32? idDocumentoWebRendicion)
        {
            return new SICER_WEBEntities1().DocumentoWebRendicion.Find(idDocumentoWebRendicion);
        }

        public void AddUpdateDocumentoRendicion(DocumentoWebRendicionBE documentoRendicionBE)
        {
            var dataContext = new SICER_WEBEntities1();
            DocumentoWebRendicion documentoWebRendicion;
            Boolean isUpdate = dataContext.DocumentoWebRendicion.Find(documentoRendicionBE.IdDocumentoWebRendicion) == null ? false : true;

            if (!isUpdate)
            {
                documentoWebRendicion = new DocumentoWebRendicion();

                documentoWebRendicion.IdDocumentoWeb = documentoRendicionBE.IdDocumentoWeb;
                documentoWebRendicion.NumeroRendicion = dataContext.DocumentoWeb.Find(documentoRendicionBE.IdDocumentoWeb).NumeroRendicion;
                documentoWebRendicion.EstadoRendicion = (Int32)EstadoDocumentoRendicion.Guardado;
                documentoWebRendicion.CreateDate = DateTime.Now;
                documentoWebRendicion.IdUsuarioCreacion = documentoRendicionBE.UserCreate.Value;
                documentoWebRendicion.IdUsuarioModificacion = documentoRendicionBE.UserCreate.Value;
            }
            else
            {
                documentoWebRendicion = dataContext.DocumentoWebRendicion.Find(documentoRendicionBE.IdDocumentoWebRendicion);
                documentoWebRendicion.IdUsuarioModificacion = documentoRendicionBE.UserUpdate.Value;
            }

            documentoWebRendicion.CorrelativoDoc = documentoRendicionBE.CorrelativoDoc;
            documentoWebRendicion.FechaDoc = documentoRendicionBE.FechaDoc;
            documentoWebRendicion.IdMonedaDoc = documentoRendicionBE.IdMonedaDoc;
            documentoWebRendicion.IdProveedor = documentoRendicionBE.IdProveedor;
            documentoWebRendicion.IdTipoDocSunat = Convert.ToInt32(documentoRendicionBE.TipoDoc);
            documentoWebRendicion.MontoAfecto = documentoRendicionBE.MontoAfecto;
            documentoWebRendicion.MontoNoAfecto = documentoRendicionBE.MontoNoAfecto;
            documentoWebRendicion.MontoTasaCambio = documentoRendicionBE.TasaCambio;
            documentoWebRendicion.MontoDoc = documentoRendicionBE.MontoDoc;
            documentoWebRendicion.MontoTotal = documentoRendicionBE.MontoTotal;
            documentoWebRendicion.MontoIGV = documentoRendicionBE.MontoIGV;
            documentoWebRendicion.SAPCodigoCentroCostos1 = documentoRendicionBE.IdCentroCostos1;
            documentoWebRendicion.SAPCodigoCentroCostos2 = documentoRendicionBE.IdCentroCostos2;
            documentoWebRendicion.SAPCodigoCentroCostos3 = documentoRendicionBE.IdCentroCostos3;
            documentoWebRendicion.SAPCodigoCentroCostos4 = documentoRendicionBE.IdCentroCostos4;
            documentoWebRendicion.SAPCodigoCentroCostos5 = documentoRendicionBE.IdCentroCostos5;
            documentoWebRendicion.SAPCodigoConcepto = documentoRendicionBE.IdConcepto;
            documentoWebRendicion.SAPCodigoPartidaPresupuestal = documentoRendicionBE.CodigoPartidaPresupuestal;
            documentoWebRendicion.SerieDoc = documentoRendicionBE.SerieDoc;
            documentoWebRendicion.SAPCodigoCuentaContableRendicion = documentoRendicionBE.CodigoCuentaContableDevolucion;

            if (isUpdate)
                dataContext.Entry(documentoWebRendicion);
            else
                dataContext.DocumentoWebRendicion.Add(documentoWebRendicion);
            dataContext.SaveChanges();
        }

        public void DeleteDocumentoRendicion(Int32 idDocumentoRendicion)
        {
            var dataContext = new SICER_WEBEntities1();
            var document = dataContext.DocumentoWebRendicion.Find(idDocumentoRendicion);
            dataContext.DocumentoWebRendicion.Remove(document);
            dataContext.SaveChanges();
        }

        public void ChangeStateDocumentoWebRendicion(Int32 idDocumentoWebRendicion, EstadoDocumentoRendicion newState)
        {
            var dataContext = new SICER_WEBEntities1();
            DocumentoWebRendicion documentoWebRendicion = dataContext.DocumentoWebRendicion.Find(idDocumentoWebRendicion);
            documentoWebRendicion.EstadoRendicion = ((Int32)newState);
            dataContext.Entry(documentoWebRendicion);
            dataContext.SaveChanges();

        }

        #endregion

        #region MigrateDocument

        public void MigrateToInterDB(DocumentoWeb documentoWeb)
        {
            //Migrate Proveedores que estén agregados en el documento
            String _ControlAccount = new UtilDA().GetControlAccount((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb, documentoWeb.Moneda.Descripcion);
            String _AccountCode = new UtilDA().GetAccountCode((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb, documentoWeb.Moneda.Descripcion);

            if ((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb == TipoDocumentoWeb.Reembolso)
                _AccountCode = new UtilDA().GetAccountCode((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb, documentoWeb.Moneda.Descripcion);

            Int32 _Etapa = 1;
            Decimal _DocRate = new UtilDA().GetRate(documentoWeb.Moneda.Descripcion);

            String _MetodoPago = null;
            if (documentoWeb.IdMetodoPago != null)
                _MetodoPago = new MetodoPagoDA().ObtenerMetodoPago(documentoWeb.IdMetodoPago.Value).PayMethCod;

            Int32 _Series = new UtilDA().GetSeries(TipoDocumentoSunat.ReciboInterno);
            Int32 _intNumber = Convert.ToInt32(documentoWeb.Codigo.Substring(2, documentoWeb.Codigo.Length - 2));


            FacturasWebMigracion facturasWebMigracion = new FacturasWebMigracion();
            facturasWebMigracion.AccountCode = _AccountCode;
            facturasWebMigracion.Asunto = documentoWeb.Asunto;
            facturasWebMigracion.CardCode = documentoWeb.Usuario2.CardCode;
            facturasWebMigracion.CardName = documentoWeb.Usuario2.CardName;
            facturasWebMigracion.CntPerson = null;
            facturasWebMigracion.Code = documentoWeb.Codigo;
            facturasWebMigracion.ControlAccount = _ControlAccount;
            facturasWebMigracion.CostingCode = documentoWeb.SAPCodigoCentroCostos1;
            facturasWebMigracion.CostingCode2 = documentoWeb.SAPCodigoCentroCostos2;
            facturasWebMigracion.CostingCode3 = documentoWeb.SAPCodigoCentroCostos3;
            facturasWebMigracion.CostingCode4 = documentoWeb.SAPCodigoCentroCostos4;
            facturasWebMigracion.CostingCode5 = documentoWeb.SAPCodigoCentroCostos5;
            facturasWebMigracion.Description = documentoWeb.Codigo;
            facturasWebMigracion.DocCurrency = documentoWeb.Moneda.Descripcion;
            facturasWebMigracion.DocDate = documentoWeb.FechaContabilizacion;
            facturasWebMigracion.DocDueDate = documentoWeb.FechaContabilizacion;
            facturasWebMigracion.DocEntry = null;
            facturasWebMigracion.DocRate = _DocRate;
            facturasWebMigracion.DocSubType = (Int32)DocSubTypeSAP.oPurchaseInvoices;
            facturasWebMigracion.Etapa = _Etapa;
            facturasWebMigracion.ExCode = _intNumber;
            facturasWebMigracion.FolioNum = _intNumber;
            facturasWebMigracion.FolioPref = ((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb).GetPrefix();
            facturasWebMigracion.IdFacturaWeb = documentoWeb.IdDocumentoWeb;
            facturasWebMigracion.IdFacturaWebRendicion = null;
            facturasWebMigracion.INT_Error = null;
            facturasWebMigracion.INT_Estado = null;
            facturasWebMigracion.INT_Ref1 = null;
            facturasWebMigracion.JournalMemo = null;
            facturasWebMigracion.LineTotal = documentoWeb.MontoInicial;
            facturasWebMigracion.NumAtCard = documentoWeb.Codigo;
            facturasWebMigracion.PaymentMethod = _MetodoPago;
            facturasWebMigracion.RendicionesTotales = null;
            facturasWebMigracion.Series = _Series;
            facturasWebMigracion.TaxCode = IGVCode.IGV_EXO.ToString();
            facturasWebMigracion.TaxDate = documentoWeb.FechaSolicitud;
            facturasWebMigracion.TipoDocumento = documentoWeb.TipoDocumentoWeb;
            facturasWebMigracion.U_BPP_MDTD = TipoDocumentoSunat.ReciboInterno.GetPrefix();
            facturasWebMigracion.U_MSS_ORD = null;

            SICER_INT_SBOEntities dataContext = new SICER_INT_SBOEntities();
            dataContext.FacturasWebMigracion.Add(facturasWebMigracion);
            dataContext.SaveChanges();

        }

        public void MigrateToInterDB(DocumentoWebRendicion documentoWebRendicion)
        {
            String _ControlAccount = new UtilDA().GetControlAccount((TipoDocumentoWeb)documentoWebRendicion.DocumentoWeb.TipoDocumentoWeb, documentoWebRendicion.Moneda.Descripcion);

            String _PartidaPresupuestal = null;
            if (documentoWebRendicion.SAPCodigoPartidaPresupuestal != null)
                _PartidaPresupuestal = new UtilDA().GetPartidaPresupuestal(documentoWebRendicion.SAPCodigoPartidaPresupuestal);

            String _AccountCode = null;
            if ((TipoDocumentoSunat)documentoWebRendicion.IdTipoDocSunat == TipoDocumentoSunat.Devolucion)
                _AccountCode = new UtilDA().GetAccountCode(documentoWebRendicion.SAPCodigoCuentaContableRendicion, true);
            else
                _AccountCode = new UtilDA().GetAccountCode(documentoWebRendicion.SAPCodigoConcepto);

            String _TaxCode = documentoWebRendicion.MontoIGV == 0 ? IGVCode.IGV_EXO.ToString() : IGVCode.IGV.ToString();
            Int32 _Etapa = 2;
            String _DocCurrency = documentoWebRendicion.Moneda.Descripcion;
            Decimal _DocRate = new UtilDA().GetRate(_DocCurrency);
            Int32 _intNumber = Convert.ToInt32(documentoWebRendicion.DocumentoWeb.Codigo.Substring(2, (documentoWebRendicion.DocumentoWeb.Codigo.Length - 2)));
            Int32 _Series = new UtilDA().GetSeries((TipoDocumentoSunat)documentoWebRendicion.Documento.IdDocumento);

            FacturasWebMigracion facturasWebMigracion = new FacturasWebMigracion();
            facturasWebMigracion.AccountCode = _AccountCode;
            facturasWebMigracion.Asunto = documentoWebRendicion.DocumentoWeb.Asunto;

            if ((TipoDocumentoSunat)documentoWebRendicion.IdTipoDocSunat == TipoDocumentoSunat.Devolucion)
            {
                facturasWebMigracion.CardCode = documentoWebRendicion.DocumentoWeb.Usuario2.CardCode;
                facturasWebMigracion.CardName = documentoWebRendicion.DocumentoWeb.Usuario2.CardName;
            }
            else
            {
                facturasWebMigracion.CardCode = documentoWebRendicion.Proveedor.CardCode;
                facturasWebMigracion.CardName = documentoWebRendicion.Proveedor.CardName;
            }

            facturasWebMigracion.CntPerson = null;
            facturasWebMigracion.Code = documentoWebRendicion.DocumentoWeb.Codigo;
            facturasWebMigracion.ControlAccount = _ControlAccount;
            facturasWebMigracion.CostingCode = documentoWebRendicion.SAPCodigoCentroCostos1;
            facturasWebMigracion.CostingCode2 = documentoWebRendicion.SAPCodigoCentroCostos2;
            facturasWebMigracion.CostingCode3 = documentoWebRendicion.SAPCodigoCentroCostos3;
            facturasWebMigracion.CostingCode4 = documentoWebRendicion.SAPCodigoCentroCostos4;
            facturasWebMigracion.CostingCode5 = documentoWebRendicion.SAPCodigoCentroCostos5;
            facturasWebMigracion.Description = null;
            facturasWebMigracion.DocCurrency = _DocCurrency;
            facturasWebMigracion.DocDate = documentoWebRendicion.FechaDoc;
            facturasWebMigracion.DocDueDate = documentoWebRendicion.FechaDoc;
            facturasWebMigracion.DocEntry = null;
            facturasWebMigracion.DocRate = _DocRate;
            facturasWebMigracion.DocSubType = (Int32)DocSubTypeSAP.oPurchaseInvoices;
            facturasWebMigracion.Etapa = _Etapa;
            facturasWebMigracion.ExCode = _intNumber;
            facturasWebMigracion.FolioPref = documentoWebRendicion.SerieDoc;
            facturasWebMigracion.FolioNum = documentoWebRendicion.CorrelativoDoc;
            facturasWebMigracion.IdFacturaWeb = documentoWebRendicion.DocumentoWeb.IdDocumentoWeb;
            facturasWebMigracion.IdFacturaWebRendicion = documentoWebRendicion.IdDocumentoWebRendicion;
            facturasWebMigracion.INT_Error = null;
            facturasWebMigracion.INT_Estado = null;
            facturasWebMigracion.INT_Ref1 = null;
            facturasWebMigracion.JournalMemo = null;
            facturasWebMigracion.LineTotal = documentoWebRendicion.MontoTotal - documentoWebRendicion.MontoIGV;
            facturasWebMigracion.PaymentMethod = null;
            facturasWebMigracion.NumAtCard = documentoWebRendicion.DocumentoWeb.Codigo;
            facturasWebMigracion.RendicionesTotales = null;
            facturasWebMigracion.Series = _Series;
            facturasWebMigracion.TaxCode = _TaxCode;
            facturasWebMigracion.TaxDate = documentoWebRendicion.FechaDoc;
            facturasWebMigracion.TipoDocumento = documentoWebRendicion.DocumentoWeb.TipoDocumentoWeb;
            facturasWebMigracion.U_BPP_MDTD = documentoWebRendicion.Documento.CodigoSunat;
            facturasWebMigracion.U_MSS_ORD = _PartidaPresupuestal;


            SICER_INT_SBOEntities dataContext = new SICER_INT_SBOEntities();
            dataContext.FacturasWebMigracion.Add(facturasWebMigracion);
            dataContext.SaveChanges();
        }

        #endregion

        private String GenerateDocumentCode(TipoDocumentoWeb tipoDocumentoWeb)
        {
            Int32 initialCode = 100000;
            String finalCode = String.Empty;

            String lastCode = new SICER_WEBEntities1().DocumentoWeb.Where(x => x.TipoDocumentoWeb == (Int32)tipoDocumentoWeb).OrderByDescending(y => y.IdDocumentoWeb).FirstOrDefault()?.Codigo;

            if (String.IsNullOrEmpty(lastCode))
                lastCode = initialCode.ToString();
            else
                lastCode = lastCode.Remove(0, 2);

            finalCode = tipoDocumentoWeb.GetPrefix() + (Convert.ToInt32(lastCode) + 1);
            return finalCode;
        }

        /*
        [Obsolete]
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
    */
    }

}
