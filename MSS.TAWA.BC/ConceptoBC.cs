using System;
using System.Collections.Generic;
using System.Linq;
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
                var list = new ConceptoDA().ListarConcepto();
                list.Add(new ConceptoBE() { CuentaContable = "0", IdConcepto = "0", Descripcion = ConstantHelper.SELECCIONE });
                list = list.OrderBy(x => x.IdConcepto).ToList();
                return list;
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