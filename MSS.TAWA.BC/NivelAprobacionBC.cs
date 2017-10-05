using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class NivelAprobacionBC
    {
        public List<NivelAprobacionBE> ListarNivelAprobacion()
        {
            try
            {
                NivelAprobacionDA objDA = new NivelAprobacionDA();
                return objDA.ListarNivelAprobacion();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public NivelAprobacionBE ObtenerNivelAprobacion(int Id, int Tipo)
        {
            try
            {
                NivelAprobacionDA objDA = new NivelAprobacionDA();
                return objDA.ObtenerNivelAprobacion(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarNivelAprobacion(NivelAprobacionBE objBE)
        {
            try
            {
                NivelAprobacionDA objDA = new NivelAprobacionDA();
                return objDA.InsertarNivelAprobacion(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarNivelAprobacion(NivelAprobacionBE objBE)
        {
            try
            {
                NivelAprobacionDA objDA = new NivelAprobacionDA();
                objDA.ModificarNivelAprobacion(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
