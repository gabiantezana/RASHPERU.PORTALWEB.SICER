using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class EntregaRendirDocumentoBC
    {
        public List<EntregaRendirDocumentoBE> ListarDocumentoDetalles(int Id, int Tipo)
        {
            try
            {
                EntregaRendirDocumentoDA objDA = new EntregaRendirDocumentoDA();
                return objDA.ListarEntregaRendirDocumento(Id, Tipo);
                //return objDA.ListarUsuario(Tipo2, IdUsuario2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public EntregaRendirDocumentoBE ObtenerDocumentoDetalle(int Id, int Tipo)
        {
            try
            {
                EntregaRendirDocumentoDA objDA = new EntregaRendirDocumentoDA();
                return objDA.ObtenerEntregaRendirDocumento(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarDocumentoDetalle(EntregaRendirDocumentoBE objBE)
        {
            try
            {
                EntregaRendirDocumentoDA objDA = new EntregaRendirDocumentoDA();
                return objDA.InsertarEntregaRendirDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarDocumentoDetalle(EntregaRendirDocumentoBE objBE)
        {
            try
            {
                EntregaRendirDocumentoDA objDA = new EntregaRendirDocumentoDA();
                objDA.ModificarEntregaRendirDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarDocumentoDetalle(int IdEntregaRendirDocumento)
        {
            try
            {
                EntregaRendirDocumentoDA objDA = new EntregaRendirDocumentoDA();
                objDA.EliminarEntregaRendirDocumento(IdEntregaRendirDocumento);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
