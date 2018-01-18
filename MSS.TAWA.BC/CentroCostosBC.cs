using System;
using System.Collections.Generic;
using System.Linq;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class CentroCostosBC
    {
        public List<CentroCostosBE> ListarCentroCostos(int nivel)
        {
            try
            {
                var objDA = new CentroCostosDA();
                var list = objDA.ListarCentroCostos(nivel);
                if (nivel != 0) return list;

                list.Add(new CentroCostosBE() { Nivel = 1, CodigoSAP = "0", Concepto = string.Empty, Descripcion = "   --[Seleccione]--  " });
                list.Add(new CentroCostosBE() { Nivel = 2, CodigoSAP = "0", Concepto = string.Empty, Descripcion = "   --[Seleccione]--  " });
                list.Add(new CentroCostosBE() { Nivel = 3, CodigoSAP = "0", Concepto = string.Empty, Descripcion = "   --[Seleccione]--  " });
                list.Add(new CentroCostosBE() { Nivel = 4, CodigoSAP = "0", Concepto = string.Empty, Descripcion = "   --[Seleccione]--  " });
                list.Add(new CentroCostosBE() { Nivel = 5, CodigoSAP = "0", Concepto = string.Empty, Descripcion = "   --[Seleccione]--  " });

                var asdd =   list.OrderBy(y => y.CodigoSAP);
                return asdd.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CentroCostosBE ObtenerCentroCostos(string CodigoSAP)
        {
            try
            {
                if (string.IsNullOrEmpty(CodigoSAP))
                {
                    return new CentroCostosBE();
                }

                var objDA = new CentroCostosDA();
                return objDA.ObtenerCentroCostos(CodigoSAP);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}