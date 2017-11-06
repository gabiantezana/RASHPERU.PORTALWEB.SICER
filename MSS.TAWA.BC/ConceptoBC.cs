using System;
using System.Collections.Generic;
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
                var objDA = new ConceptoDA();
                return objDA.ListarConcepto();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ConceptoBE ObtenerConcepto(string codigo)
        {
            try
            {
                var objDA = new ConceptoDA();
                return objDA.ObtenerConcepto(codigo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}