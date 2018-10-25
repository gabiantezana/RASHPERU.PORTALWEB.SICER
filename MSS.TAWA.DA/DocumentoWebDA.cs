using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MSS.TAWA.BE;
using MSS.TAWA.MIGRATIONTOSAP;
using MSS.TAWA.MODEL;

namespace MSS.TAWA.DA
{
    public class DocumentoWebDA
    {
        public List<DocumentoWeb> GetListRendicionesPendientesReembolso(int idUsuario, string moneda)
        {
            var dataContext = new SICER_WEBEntities();
            var DocumentosWebQueSePuedenReembolsar = new List<DocumentoWeb>();

            var documentosDeUsuario = GetListDocumentoWeb(idUsuario, TipoDocumentoWeb.EntregaRendir)
                .Where(x => x.EstadoDocumento != (int)EstadoDocumento.Liquidado).ToList();
            foreach (var docmento in documentosDeUsuario)
            {
                var rendicionesASumar = docmento.DocumentoWebRendicion.ToList().Where(x =>
                    x.EstadoRendicion == (int)EstadoDocumentoRendicion.Guardado
                    || x.EstadoRendicion == (int)EstadoDocumentoRendicion.Rendido
                    && x.NumeroRendicion == docmento.NumeroRendicion
                    && x.IdMonedaDoc == Convert.ToInt32(moneda)
                ).ToList();

                var totalMontoDocumento = rendicionesASumar.Sum(x => x.MontoTotal);
                if (totalMontoDocumento > docmento.MontoInicial)
                    DocumentosWebQueSePuedenReembolsar.Add(docmento);
            }
            return DocumentosWebQueSePuedenReembolsar;
        }

        public List<DocumentoWeb> GetListDocumentoWeb(int IdUsuario, TipoDocumentoWeb tipoDocumentoWeb)
        {
            var dataContext = new SICER_WEBEntities();
            var usuario = dataContext.Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();

            if (usuario.IdPerfilUsuario == null)
                throw new Exception("El usuario no tiene ningún perfil asignado. No se pueden listar los documentos.");


            var esAdministrador = false;
            var esCreador = false;
            var esAprobador = false;
            var esContador = false;
            var esSistemas = false;
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

            var documentosCreadosPorUsuario = new List<DocumentoWeb>();
            var pendientesAprobarNivelPorUsuario = new List<DocumentoWeb>();
            var pendientesPorAprobarContabilidad = new List<DocumentoWeb>();
            var documentosDeSistemas = new List<DocumentoWeb>();
            var documentosDeAdministradores = new List<DocumentoWeb>();

            if (esSistemas)
                documentosDeSistemas = dataContext.DocumentoWeb.ToList();

            if (esAdministrador)
                documentosDeAdministradores = dataContext.DocumentoWeb.ToList();

            if (esCreador)
                documentosCreadosPorUsuario =
                    dataContext.DocumentoWeb.Where(x => x.IdUsuarioSolicitante == IdUsuario).ToList();

            if (esAprobador)
                switch (tipoDocumentoWeb)
                {
                    case TipoDocumentoWeb.CajaChica:
                        pendientesAprobarNivelPorUsuario = dataContext.DocumentoWeb.Where(x =>
                            x.Usuario.IdUsuarioCC1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel1
                            || x.Usuario.IdUsuarioCC2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel2
                            || x.Usuario.IdUsuarioCC3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel3
                            //rendiciones
                            || x.Usuario.IdUsuarioCC1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            || x.Usuario.IdUsuarioCC2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            ).ToList();
                        break;
                    case TipoDocumentoWeb.EntregaRendir:
                        pendientesAprobarNivelPorUsuario = dataContext.DocumentoWeb.Where(x =>
                            x.Usuario.IdUsuarioER1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel1
                            || x.Usuario.IdUsuarioER2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel2
                            || x.Usuario.IdUsuarioER3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel3
                            //rendiciones
                            || x.Usuario.IdUsuarioER1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            || x.Usuario.IdUsuarioER2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            || x.Usuario.IdUsuarioER3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            ).ToList();
                        break;
                    case TipoDocumentoWeb.Reembolso:
                        pendientesAprobarNivelPorUsuario = dataContext.DocumentoWeb.Where(x =>
                            x.Usuario.IdUsuarioRE1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel1
                            || x.Usuario.IdUsuarioRE2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel2
                            || x.Usuario.IdUsuarioRE3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.PorAprobarNivel3
                            //rendiciones
                            || x.Usuario.IdUsuarioRE1 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            || x.Usuario.IdUsuarioRE2 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            || x.Usuario.IdUsuarioRE3 == IdUsuario && (EstadoDocumento)x.EstadoDocumento ==
                            EstadoDocumento.RendirPorAprobarJefeArea
                            ).ToList();
                        break;
                    default:
                        throw new NotImplementedException();
                }

            if (esContador)
                pendientesPorAprobarContabilidad = dataContext.DocumentoWeb
                    .Where(x => x.EstadoDocumento == (int)EstadoDocumento.RendirPorAprobarContabilidad).ToList();


            var listDocumentoWeb = new List<DocumentoWeb>();
            listDocumentoWeb.AddRange(documentosDeSistemas);
            listDocumentoWeb.AddRange(documentosDeAdministradores);
            listDocumentoWeb.AddRange(documentosCreadosPorUsuario);
            listDocumentoWeb.AddRange(pendientesAprobarNivelPorUsuario);
            listDocumentoWeb.AddRange(pendientesPorAprobarContabilidad);

            listDocumentoWeb = listDocumentoWeb.Where(x => x.TipoDocumentoWeb == (int)tipoDocumentoWeb).Distinct()
                .ToList();

            return listDocumentoWeb?.ToList() ?? new List<DocumentoWeb>();
        }

        private string GenerateDocumentCode(TipoDocumentoWeb tipoDocumentoWeb)
        {
            var initialCode = 100000;
            var finalCode = string.Empty;

            var lastCode = new SICER_WEBEntities().DocumentoWeb
                .Where(x => x.TipoDocumentoWeb == (int)tipoDocumentoWeb).OrderByDescending(y => y.IdDocumentoWeb)
                .FirstOrDefault()?.Codigo;

            if (string.IsNullOrEmpty(lastCode))
                lastCode = initialCode.ToString();
            else
                lastCode = lastCode.Remove(0, 2);

            finalCode = tipoDocumentoWeb.GetPrefix() + (Convert.ToInt32(lastCode) + 1);
            return finalCode;
        }

        #region DocumentoWeb

        public List<DocumentoWeb> GetListDocumentosPendientesPorUsuario(int IdUsuario,
            TipoDocumentoWeb tipoDocumentoWeb)
        {
            var listadoEstadosNoPendientes = new List<EstadoDocumento>
            {
                EstadoDocumento.Aprobado,
                EstadoDocumento.Liquidado,
                EstadoDocumento.Rechazado,
                EstadoDocumento.RendirAprobado
            };
            var list = GetListDocumentoWeb(IdUsuario, tipoDocumentoWeb).Where(x =>
                !listadoEstadosNoPendientes.Contains((EstadoDocumento)x.EstadoDocumento));
            return list.ToList();
        }

        public DocumentoWeb GetDocumentoWeb(int? idDocumentoWeb)
        {
            var item = new SICER_WEBEntities().DocumentoWeb.Find(idDocumentoWeb);
            return item;
        }

        public void AddUpdateDocumentoWeb(DocumentoWebBE documentoWebBE)
        {
            var dataContext = new SICER_WEBEntities();
            DocumentoWeb documentoWeb;
            var isUpdate = dataContext.DocumentoWeb.Find(documentoWebBE.IdDocumentoWeb) == null ? false : true;

            if (!isUpdate)
            {
                documentoWeb = new DocumentoWeb();
                documentoWeb.Codigo = GenerateDocumentCode(documentoWebBE.TipoDocumentoWeb);
                documentoWeb.EstadoDocumento = (int)EstadoDocumento.PorAprobarNivel1;
                documentoWeb.NumeroRendicion = 1;
                documentoWeb.CreateDate = DateTime.Now;
                documentoWeb.UpdateDate = DateTime.Now;
                documentoWeb.IdUsuarioCreacion = Convert.ToInt32(documentoWebBE.IdUsuarioCreador);
                documentoWeb.IdUsuarioSolicitante = documentoWebBE.IdUsuarioSolicitante;
                documentoWeb.IdUsuarioModificacion = documentoWeb.IdUsuarioCreacion;
                documentoWeb.TipoDocumentoWeb = (int)documentoWebBE.TipoDocumentoWeb;
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
            var datacontext = new SICER_WEBEntities();
            using (var trx = datacontext.Database.BeginTransaction())
            {
                try
                {
                    var documentoWeb = datacontext.DocumentoWeb.Find(cambioEstadoBE.IdDocumentoWeb);

                    var estadoActualDocumento = (EstadoDocumento)Convert.ToInt32(documentoWeb.EstadoDocumento);
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

                    documentoWeb.EstadoDocumento = (int)nuevoEstado;
                    datacontext.Entry(documentoWeb);
                    datacontext.SaveChanges();

                    //GUARDA AUDITORÍA
                    var documentoWebAuditoria = new DocumentoWebAuditoria();
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
                            var entregaRendirPorLiquidar =
                                datacontext.DocumentoWeb.Find(documentoWeb.IdDocumentoWebRendicionReferencia);
                            entregaRendirPorLiquidar.EstadoDocumento = (int)EstadoDocumento.Liquidado;
                            datacontext.Entry(entregaRendirPorLiquidar);
                            datacontext.SaveChanges();
                        }

                        MigrateToInterDB(documentoWeb, out FacturasWebMigracion facturaWebMigrada);
                        new PublishToSap().PublishRendicionesToSap(facturaWebMigrada);

                    }

                    //SI LAS RENDICIONES SON APROBADAS, SE MIGRAN LAS RENDICIONES A LA BD INTERMEDIA.
                    else if (nuevoEstado == EstadoDocumento.RendirAprobado)
                    {
                        //MIGRA BUSINESS PARTNERS
                        var list = new SICER_WEBEntities().Proveedor.Where(x => x.IdProceso == documentoWeb.IdDocumentoWeb).ToList();

                        MigrateBusinessPartner(list, out List<MaestroTrabajadores> businessPartnerMigratedList);

                        var ListDocumentosGuardados = documentoWeb.DocumentoWebRendicion.Where(x =>
                            x.NumeroRendicion == documentoWeb.NumeroRendicion
                            && x.EstadoRendicion == (int)EstadoDocumentoRendicion.Guardado).ToList();
                        var documentsMigratedList = new List<FacturasWebMigracion>();
                        foreach (var documentoWebRendicion in ListDocumentosGuardados)
                        {
                            documentoWebRendicion.EstadoRendicion = (int)EstadoDocumentoRendicion.Rendido;
                            datacontext.Entry(documentoWebRendicion);
                            datacontext.SaveChanges();

                            //MIGRA RENDICIONES
                            MigrateToInterDB(documentoWebRendicion, out FacturasWebMigracion facturasWebMigracion);
                            documentsMigratedList.Add(facturasWebMigracion);
                        }
                        new PublishToSap().PublishRendicionesToSap(businessPartnerMigratedList, documentsMigratedList);
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
            var datacontext = new SICER_WEBEntities();
            var documentoWeb = datacontext.DocumentoWeb.Find(cambioEstadoBE.IdDocumentoWeb);

            switch ((EstadoDocumento)documentoWeb.EstadoDocumento)
            {
                case EstadoDocumento.Aprobado:
                case EstadoDocumento.Liquidado:
                case EstadoDocumento.Rechazado:
                case EstadoDocumento.RendirAprobado:
                    throw new Exception("El documento no se puede rechazar. El estado actual del documento es: " +
                                        (EstadoDocumento)documentoWeb.EstadoDocumento);
                case EstadoDocumento.RendirPorAprobarContabilidad:
                case EstadoDocumento.RendirPorAprobarJefeArea:
                default:
                    break;
            }

            documentoWeb.EstadoDocumento = (int)EstadoDocumento.Rechazado;
            var rendicionesARechazar = documentoWeb.DocumentoWebRendicion.Where(x =>
                x.NumeroRendicion == documentoWeb.NumeroRendicion &&
                x.EstadoRendicion == (int)EstadoDocumentoRendicion.Guardado).ToList();

            foreach (var documentoRendicion in rendicionesARechazar)
            {
                documentoRendicion.EstadoRendicion = (int)EstadoDocumentoRendicion.Rechazado;
                datacontext.Entry(documentoRendicion);
            }

            datacontext.Entry(documentoWeb);

            //GUARDA AUDITORÍA
            var documentoWebAuditoria = new DocumentoWebAuditoria();
            documentoWebAuditoria.IdDocumentoWeb = cambioEstadoBE.IdDocumentoWeb;
            documentoWebAuditoria.IdUsuario = cambioEstadoBE.IdUsuario;
            documentoWebAuditoria.Comentario = cambioEstadoBE.Comentario;
            documentoWebAuditoria.EstadoDocumento = documentoWeb.EstadoDocumento;
            documentoWebAuditoria.Fecha = DateTime.Now;
            datacontext.DocumentoWebAuditoria.Add(documentoWebAuditoria);

            datacontext.SaveChanges();
        }

        public void EnviarRendicion(int idDocumentoWeb)
        {
            var dataContext = new SICER_WEBEntities();
            var documentoWeb = dataContext.DocumentoWeb.Find(idDocumentoWeb);

            //Cambia estado a pendiente de rendición
            documentoWeb.EstadoDocumento = (int)EstadoDocumento.RendirPorAprobarJefeArea;
            dataContext.Entry(documentoWeb);

            dataContext.SaveChanges();
        }

        public void MigrateBusinessPartner(List<Proveedor> list, out List<MaestroTrabajadores> outList)
        {
            outList = new List<MaestroTrabajadores>();
            foreach (var item in list)
            {
                var maestroTrabajadores = new MaestroTrabajadores();
                var proveedor = new SICER_WEBEntities().Proveedor
                    .Where(x => x.CardCode == item.Documento && x.CardName == item.CardName).FirstOrDefault();
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

                    var datacontext = new SICER_WEBEntities();
                    datacontext.MaestroTrabajadores.Add(maestroTrabajadores);
                    datacontext.SaveChanges();

                    outList.Add(maestroTrabajadores);
                }
            }
        }

        public void AprobarYLiquidarDocumento(CambioEstadoBE cambioEstadoBE)
        {
            var dataContext = new SICER_WEBEntities();
            AprobarDocumento(cambioEstadoBE);

            //Liquida documento
            var documentoWeb = dataContext.DocumentoWeb.Find(cambioEstadoBE.IdDocumentoWeb);
            documentoWeb.EstadoDocumento = (int)EstadoDocumento.Liquidado;
            documentoWeb.NumeroRendicion += 1;
            dataContext.Entry(documentoWeb);

            //GUARDA AUDITORÍA
            var documentoWebAuditoria = new DocumentoWebAuditoria();
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

        public List<DocumentoWebRendicion> GetListDocumentoWebRendicion(int IdDocumentoWeb)
        {
            var list = new SICER_WEBEntities().DocumentoWebRendicion.Where(x =>
                x.IdDocumentoWeb == IdDocumentoWeb && x.DocumentoWeb.NumeroRendicion == x.NumeroRendicion).ToList();
            return list;
        }

        public DocumentoWebRendicion GetDocumentoWebRendicion(int? idDocumentoWebRendicion)
        {
            return new SICER_WEBEntities().DocumentoWebRendicion.Find(idDocumentoWebRendicion);
        }

        public void AddUpdateDocumentoRendicion(DocumentoWebRendicionBE documentoRendicionBE)
        {
            var dataContext = new SICER_WEBEntities();
            DocumentoWebRendicion documentoWebRendicion;
            var isUpdate = dataContext.DocumentoWebRendicion.Find(documentoRendicionBE.IdDocumentoWebRendicion) == null
                ? false
                : true;

            if (!isUpdate)
            {
                documentoWebRendicion = new DocumentoWebRendicion();

                documentoWebRendicion.IdDocumentoWeb = documentoRendicionBE.IdDocumentoWeb;
                documentoWebRendicion.NumeroRendicion = dataContext.DocumentoWeb
                    .Find(documentoRendicionBE.IdDocumentoWeb).NumeroRendicion;
                documentoWebRendicion.EstadoRendicion = (int)EstadoDocumentoRendicion.Guardado;
                documentoWebRendicion.CreateDate = DateTime.Now;
                documentoWebRendicion.IdUsuarioCreacion = documentoRendicionBE.UserCreate.Value;
                documentoWebRendicion.IdUsuarioModificacion = documentoRendicionBE.UserCreate.Value;
            }
            else
            {
                documentoWebRendicion =
                    dataContext.DocumentoWebRendicion.Find(documentoRendicionBE.IdDocumentoWebRendicion);
                documentoWebRendicion.IdUsuarioModificacion = documentoRendicionBE.UserUpdate.Value;
            }

            documentoWebRendicion.CorrelativoDoc = documentoRendicionBE.CorrelativoDoc;
            documentoWebRendicion.FechaDoc = documentoRendicionBE.FechaDoc;
            documentoWebRendicion.IdMonedaDoc = documentoRendicionBE.IdMonedaDoc;
            if (string.IsNullOrEmpty(documentoRendicionBE.SAPProveedor))
                throw new Exception("El proveedor de SAP se está enviando nulo. Contacte a su administrador");
            documentoWebRendicion.SAPProveedor = documentoRendicionBE.SAPProveedor;
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
            documentoWebRendicion.SAPCodigoCuentaContableRendicion =
                documentoRendicionBE.CodigoCuentaContableDevolucion;

            if (isUpdate)
                dataContext.Entry(documentoWebRendicion);
            else
                dataContext.DocumentoWebRendicion.Add(documentoWebRendicion);
            dataContext.SaveChanges();
        }

        public void DeleteDocumentoRendicion(int idDocumentoRendicion)
        {
            var dataContext = new SICER_WEBEntities();
            var document = dataContext.DocumentoWebRendicion.Find(idDocumentoRendicion);
            if (document == null)
                throw new Exception("No se encontró ningún documento con id de Rendición: " + idDocumentoRendicion);


            dataContext.DocumentoWebRendicion.Remove(document);
            dataContext.SaveChanges();
        }

        public void ChangeStateDocumentoWebRendicion(int idDocumentoWebRendicion, EstadoDocumentoRendicion newState)
        {
            var dataContext = new SICER_WEBEntities();
            var documentoWebRendicion = dataContext.DocumentoWebRendicion.Find(idDocumentoWebRendicion);
            documentoWebRendicion.EstadoRendicion = (int)newState;
            dataContext.Entry(documentoWebRendicion);
            dataContext.SaveChanges();
        }

        #endregion

        #region MigrateDocument

        public void MigrateToInterDB(DocumentoWeb documentoWeb, out FacturasWebMigracion facturasWebMigracion)
        {
            //Migrate Proveedores que estén agregados en el documento
            var _ControlAccount = new UtilDA().GetControlAccount((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb,
                documentoWeb.Moneda.Descripcion);
            var _AccountCode = new UtilDA().GetAccountCode((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb,
                documentoWeb.Moneda.Descripcion);

            if ((TipoDocumentoWeb)documentoWeb.TipoDocumentoWeb == TipoDocumentoWeb.Reembolso)
                _AccountCode = new SICER_WEBEntities().FacturasWebMigracion
                    .Where(x => x.IdFacturaWeb == documentoWeb.IdDocumentoWebRendicionReferencia).FirstOrDefault()
                    .AccountCode;

            var _Etapa = 1;

            var _DocRate = 1M;
            var monedasNacionales = new[] { "SOL", "S/", "S/.", "PEN", "SOLES" };
            if (!monedasNacionales.Contains(documentoWeb.Moneda.Descripcion))
                _DocRate = new UtilDA().GetRate(documentoWeb.Moneda.Descripcion);

            string _MetodoPago = null;
            if (documentoWeb.IdMetodoPago != null)
                _MetodoPago = new MetodoPagoDA().ObtenerMetodoPago(documentoWeb.IdMetodoPago.Value).PayMethCod;


            var _intNumber = Convert.ToInt32(documentoWeb.Codigo.Substring(2, documentoWeb.Codigo.Length - 2));


            facturasWebMigracion = new FacturasWebMigracion();
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
            facturasWebMigracion.DocSubType = (int)DocSubTypeSAP.oPurchaseInvoices;
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
            facturasWebMigracion.TaxCode = IGVCode.IGV_EXO.GetIGVCode();
            facturasWebMigracion.TaxDate = documentoWeb.FechaSolicitud;
            facturasWebMigracion.TipoDocumento = documentoWeb.TipoDocumentoWeb;
            facturasWebMigracion.U_BPP_MDTD = TipoDocumentoSunat.ReciboInterno.GetCodigoSunat();
            facturasWebMigracion.U_MSS_ORD = null;

            if ((EmpresaInterna)Convert.ToInt32(ConfigurationManager.AppSettings[ConstantHelper.Keys.IdEmpresaInterna]) == EmpresaInterna.IIMP)
                facturasWebMigracion.Series = new UtilDA().GetSeries(TipoDocumentoSunat.ReciboInterno);

            var dataContext = new SICER_WEBEntities();
            dataContext.FacturasWebMigracion.Add(facturasWebMigracion);
            dataContext.SaveChanges();

        }

        public void MigrateToInterDB(DocumentoWebRendicion documentoWebRendicion, out FacturasWebMigracion outFacturaWebMigracion)
        {
            var _ControlAccount = new UtilDA().GetControlAccount(
                (TipoDocumentoWeb)documentoWebRendicion.DocumentoWeb.TipoDocumentoWeb,
                documentoWebRendicion.Moneda.Descripcion, true);

            string _AccountCode = null;
            if ((TipoDocumentoSunat)documentoWebRendicion.IdTipoDocSunat == TipoDocumentoSunat.Devolucion)
                _AccountCode = new UtilDA().GetAccountCode(documentoWebRendicion.SAPCodigoCuentaContableRendicion, true);
            else
                _AccountCode = new UtilDA().GetAccountCode(documentoWebRendicion.SAPCodigoConcepto);

            var _TaxCode = documentoWebRendicion.MontoIGV == 0 ? IGVCode.IGV_EXO.GetIGVCode() : IGVCode.IGV.GetIGVCode();
            var _Etapa = 2;
            var _DocCurrency = documentoWebRendicion.Moneda.Descripcion;
            var _DocRate = 1M;
            var monedasNacionales = new[] { "SOL", "S/", "S/.", "PEN", "SOLES" };
            if (!monedasNacionales.Contains(documentoWebRendicion.Moneda.Descripcion))
                _DocRate = new UtilDA().GetRate(documentoWebRendicion.Moneda.Descripcion);

            var _intNumber =
                Convert.ToInt32(documentoWebRendicion.DocumentoWeb.Codigo.Substring(2,
                    documentoWebRendicion.DocumentoWeb.Codigo.Length - 2));

            var facturasWebMigracion = new FacturasWebMigracion();
            facturasWebMigracion.AccountCode = _AccountCode;
            facturasWebMigracion.Asunto = documentoWebRendicion.DocumentoWeb.Asunto;

            if ((TipoDocumentoSunat)documentoWebRendicion.IdTipoDocSunat == TipoDocumentoSunat.Devolucion)
            {
                facturasWebMigracion.CardCode = documentoWebRendicion.DocumentoWeb.Usuario2.CardCode;
                facturasWebMigracion.CardName = documentoWebRendicion.DocumentoWeb.Usuario2.CardName;
            }
            else
            {
                facturasWebMigracion.CardCode = documentoWebRendicion.SAPProveedor;
                facturasWebMigracion.CardName = string.Empty;//TODO:?
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
            facturasWebMigracion.DocSubType = (int)DocSubTypeSAP.oPurchaseInvoices;
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
            facturasWebMigracion.TaxCode = _TaxCode;
            facturasWebMigracion.TaxDate = documentoWebRendicion.FechaDoc;
            facturasWebMigracion.TipoDocumento = documentoWebRendicion.DocumentoWeb.TipoDocumentoWeb;
            facturasWebMigracion.U_BPP_MDTD = documentoWebRendicion.Documento.CodigoSunat;


            if ((EmpresaInterna)Convert.ToInt32(ConfigurationManager.AppSettings[ConstantHelper.Keys.IdEmpresaInterna]) == EmpresaInterna.IIMP)
            {
                facturasWebMigracion.Series = new UtilDA().GetSeries(TipoDocumentoSunat.ReciboInterno);

                if (documentoWebRendicion.SAPCodigoPartidaPresupuestal != null)
                    facturasWebMigracion.U_MSS_ORD = new UtilDA().GetPartidaPresupuestal(documentoWebRendicion.SAPCodigoPartidaPresupuestal);
            }


            var dataContext = new SICER_WEBEntities();
            dataContext.FacturasWebMigracion.Add(facturasWebMigracion);
            dataContext.SaveChanges();

            outFacturaWebMigracion = facturasWebMigracion;
        }
        #endregion
    }
}