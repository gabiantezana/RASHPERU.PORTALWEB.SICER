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
        public List<CentroCostosBE> ListarCentroCostos(int Id, int Tipo, int Tipo2)
        {
            try
            {
                CentroCostosDA objDA = new CentroCostosDA();
                return objDA.ListarCentroCostos(Id, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }
    }
}
