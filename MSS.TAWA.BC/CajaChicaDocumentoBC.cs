using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class CajaChicaDocumentoBC
    {
        public List<CajaChicaDocumentoBE> ListarCajaChicaDocumento(int Id, int Tipo, int Tipo2)
        {
            try
            {
                CajaChicaDocumentoDA objDA = new CajaChicaDocumentoDA();
                return objDA.ListarCajaChicaDocumento(Id, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CajaChicaDocumentoBE ObtenerCajaChicaDocumento(int Id, int Tipo)
        {
            try
            {
                CajaChicaDocumentoDA objDA = new CajaChicaDocumentoDA();
                return objDA.ObtenerCajaChicaDocumento(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarCajaChicaDocumento(CajaChicaDocumentoBE objBE)
        {
            try
            {
                CajaChicaDocumentoDA objDA = new CajaChicaDocumentoDA();
                return objDA.InsertarCajaChicaDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarCajaChicaDocumento(CajaChicaDocumentoBE objBE)
        {
            try
            {
                CajaChicaDocumentoDA objDA = new CajaChicaDocumentoDA();
                objDA.ModificarCajaChicaDocumento(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarCajaChicaDocumento(int IdCajaChicaDocumento)
        {
            try
            {
                CajaChicaDocumentoDA objDA = new CajaChicaDocumentoDA();
                objDA.EliminarCajaChicaDocumento(IdCajaChicaDocumento);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
