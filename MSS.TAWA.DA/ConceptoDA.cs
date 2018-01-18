using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class ConceptoDA
    {
        // Listar Concepto
        public List<ConceptoBE> ListarConcepto()
        {
            var url = "concepts/getconceptslist.xsjs";
            var response = UtilDA.GetJsonResponse(url, typeof(List<ConceptoBE>));
           
            return response;
        }

        // Obtener Concepto
        public ConceptoBE ObtenerConcepto(string codigo)
        {
            var url = "concepts/getconcepts.xsjs?code=" + codigo;
            var response = UtilDA.GetJsonResponse(url, typeof(ConceptoBE));
            return response;
        }
    }
}