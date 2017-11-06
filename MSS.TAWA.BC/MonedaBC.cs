using System;
using System.Collections.Generic;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class MonedaBC
    {
        public List<MonedaBE> ListarMoneda(int? IdDocumentoWeb = null)
        {
            try
            {
                var objDA = new MonedaDA();
                return objDA.ListarMoneda(IdDocumentoWeb);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MonedaBE ObtenerMoneda(int Id)
        {
            try
            {
                var objDA = new MonedaDA();
                return objDA.ObtenerMoneda(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}