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
        public List<ConceptoBE> ListarConcepto(int Id, int Tipo)
        {
            try
            {
                ConceptoDA objDA = new ConceptoDA();
                return objDA.ListarConcepto(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ConceptoBE ObtenerConcepto(int Id)
        {
            try
            {
                ConceptoDA objDA = new ConceptoDA();
                return objDA.ObtenerConcepto(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
