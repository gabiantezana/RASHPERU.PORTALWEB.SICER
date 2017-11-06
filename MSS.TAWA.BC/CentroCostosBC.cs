using System;
using System.Collections.Generic;
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
                return objDA.ListarCentroCostos(nivel);
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