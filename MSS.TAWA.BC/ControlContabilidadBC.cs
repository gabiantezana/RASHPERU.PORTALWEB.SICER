using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class ControlContabilidadBC
    {
        public bool InsertarReembolso(ControlContabilidadBE objBE)
        {
            try
            {
                ControlContabilidadDA objDA = new ControlContabilidadDA();
                return objDA.InsertarControlContabilidad(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
