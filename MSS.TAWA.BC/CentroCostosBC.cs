using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class CentroCostosBC
    {
        public List<CentroCostosBE> ListarCentroCostos(int IdEmpresa, int nivel)
        {
            try
            {
                CentroCostosDA objDA = new CentroCostosDA();
                return objDA.ListarCentroCostos(IdEmpresa, nivel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CentroCostosBE ObtenerCentroCostos(int CodigoSAP)
        {
            try
            {
                CentroCostosDA objDA = new CentroCostosDA();
                return objDA.ObtenerCentroCostos(CodigoSAP);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
