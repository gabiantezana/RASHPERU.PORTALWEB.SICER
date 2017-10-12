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
        public List<EntregaRendirDocumentoBE> ListarEntregaRendirDocumento(int Id, int Tipo)
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

        public EntregaRendirDocumentoBE ObtenerEntregaRendirDocumento(int Id, int Tipo)
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

        public int InsertarEntregaRendirDocumento(EntregaRendirDocumentoBE objBE)
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

        public void ModificarEntregaRendirDocumento(EntregaRendirDocumentoBE objBE)
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

        public void EliminarEntregaRendirDocumento(int IdEntregaRendirDocumento)
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
