using System;
using System.Collections.Generic;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class DocumentoBC
    {
        public List<DocumentobBE> ListarDocumento(int Id, int Tipo)
        {
            try
            {
                var objDA = new DocumentoDA();
                return objDA.ListarDocumento(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DocumentobBE ObtenerDocumento(int Id)
        {
            try
            {
                var objDA = new DocumentoDA();
                return objDA.ObtenerDocumento(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}