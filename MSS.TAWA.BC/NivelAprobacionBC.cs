using System;
using System.Collections.Generic;
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
                var objDA = new NivelAprobacionDA();
                return objDA.ListarNivelAprobacion();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NivelAprobacionBE ObtenerNivelAprobacion(int Id, int Tipo)
        {
            try
            {
                var objDA = new NivelAprobacionDA();
                return objDA.ObtenerNivelAprobacion(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarNivelAprobacion(NivelAprobacionBE objBE)
        {
            try
            {
                var objDA = new NivelAprobacionDA();
                return objDA.InsertarNivelAprobacion(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarNivelAprobacion(NivelAprobacionBE objBE)
        {
            try
            {
                var objDA = new NivelAprobacionDA();
                objDA.ModificarNivelAprobacion(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}