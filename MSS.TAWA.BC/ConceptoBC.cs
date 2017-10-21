using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class ConceptoBC
    {
        public List<ConceptoBE> ListarConcepto()
        {
            try
            {
                ConceptoDA objDA = new ConceptoDA();
                return objDA.ListarConcepto();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ConceptoBE ObtenerConcepto(String codigo)
        {
            try
            {
                ConceptoDA objDA = new ConceptoDA();
                return objDA.ObtenerConcepto(codigo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
