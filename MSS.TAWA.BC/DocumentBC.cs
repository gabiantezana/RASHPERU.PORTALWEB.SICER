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
        public List<DocumentBE> ListarDocumentos(int IdUsuario, int Tipo, int Tipo2, String CodigoDocumento, String Dni, String NombreSolicitante, String EsFacturable, String Estado)
        {
            try
            {
                DocumentDA objDA = new DocumentDA();
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
                DocumentDA objDA = new DocumentDA();
                return objDA.ObtenerDocumento(IdDocumento, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertarDocumento(DocumentBE objBE)
        {
            try
            {
                DocumentDA objDA = new DocumentDA();
                objDA.AddUpdateDocumentoWeb(objBE);
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
                DocumentDA objDA = new DocumentDA();
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
                DocumentDA objDA = new DocumentDA();
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
                DocumentDA objDA = new DocumentDA();
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
                DocumentDA objDA = new DocumentDA();
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
                DocumentDA objDA = new DocumentDA();
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
                DocumentDA objDA = new DocumentDA();
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
