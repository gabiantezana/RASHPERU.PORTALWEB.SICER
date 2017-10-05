using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class ReembolsoDocumentoBC
    {
        public List<ReembolsoDocumentoBE> ListarReembolsoDocumento(int Id, int Tipo)
        {
            try
            {
                ReembolsoDocumentoDA objDA = new ReembolsoDocumentoDA();
                return objDA.ListarReembolsoDocumento(Id, Tipo);
                //return objDA.ListarUsuario(Tipo2, IdUsuario2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ReembolsoDocumentoBE ObtenerReembolsoDocumento(int Id, int Tipo)
        {
            try
            {
                ReembolsoDocumentoDA objDA = new ReembolsoDocumentoDA();
                return objDA.ObtenerReembolsoDocumento(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarReembolsoDocumento(ReembolsoDocumentoBE objBE)
        {
            try
            {
                ReembolsoDocumentoDA objDA = new ReembolsoDocumentoDA();
                return objDA.InsertarReembolsoDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarReembolsoDocumento(ReembolsoDocumentoBE objBE)
        {
            try
            {
                ReembolsoDocumentoDA objDA = new ReembolsoDocumentoDA();
                objDA.ModificarReembolsoDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarReembolsoDocumento(int IdReembolsoDocumento)
        {
            try
            {
                ReembolsoDocumentoDA objDA = new ReembolsoDocumentoDA();
                objDA.EliminarReembolsoDocumento(IdReembolsoDocumento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
