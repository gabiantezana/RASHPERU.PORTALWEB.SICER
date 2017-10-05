using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class DocumentoBC
    {
        public List<DocumentoBE> ListarDocumento(int Id, int Tipo)
        {
            try
            {
                DocumentoDA objDA = new DocumentoDA();
                return objDA.ListarDocumento(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DocumentoBE ObtenerDocumento(int Id)
        {
            try
            {
                DocumentoDA objDA = new DocumentoDA();
                return objDA.ObtenerDocumento(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
