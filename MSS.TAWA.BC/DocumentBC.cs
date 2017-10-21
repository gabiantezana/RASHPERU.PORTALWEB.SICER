using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class DocumentBC
    {
        public DocumentBC(TipoDocumentoWeb tipoDocumento)
        {
            _TipoDocumento = tipoDocumento;
        }

        private TipoDocumentoWeb _TipoDocumento { get; set; }

        public List<DocumentBE> ListarDocumentos(int IdUsuario, int Tipo, int Tipo2, String CodigoDocumento, String Dni, String NombreSolicitante, String EsFacturable, String Estado)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                return objDA.ListarDocumentos(IdUsuario, Tipo, Tipo2, CodigoDocumento, Dni, NombreSolicitante, EsFacturable, Estado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DocumentBE ObtenerDocumento(int IdDocumento, int Tipo)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                return objDA.ObtenerDocumento(IdDocumento, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarDocumento(DocumentBE objBE)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                return objDA.InsertarDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarDocumento(DocumentBE objBE)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                objDA.ModificarDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Detalle Documento

        public List<DocumentDetailBE> ListarDocumentoDetalles(int Id, int Tipo, int Tipo2)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                return objDA.ListarDocumentoDetalle(Id, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DocumentDetailBE ObtenerDocumentoDetalle(int Id, int Tipo)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                return objDA.ObtenerDocumentoDetalle(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarDocumentoDetalle(DocumentDetailBE objBE)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                return objDA.InsertarDocumentoDetalle(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarDocumentoDetalle(DocumentDetailBE objBE)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                objDA.ModificarDocumentoDetalle(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarDocumentoDetalle(int idDocumentoDetalle)
        {
            try
            {
                DocumentDA objDA = new DocumentDA(_TipoDocumento);
                objDA.EliminarDocumentoDetalle(idDocumentoDetalle);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
