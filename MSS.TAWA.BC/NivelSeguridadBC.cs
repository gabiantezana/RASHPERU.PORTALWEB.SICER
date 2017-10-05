using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class NivelSeguridadBC
    {
        public NivelSeguridadBE ObtenerNivelSeguridad()
        {
            try
            {
                NivelSeguridadDA objDA = new NivelSeguridadDA();
                return objDA.ObtenerNivelSeguridad();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarNivelAprobacion(NivelSeguridadBE objBE)
        {
            try
            {
                NivelSeguridadDA objDA = new NivelSeguridadDA();
                objDA.ModificarNivelSeguridad(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
