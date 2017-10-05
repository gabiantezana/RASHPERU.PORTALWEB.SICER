using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class AprobacionBC
    {
        public List<AprobacionBE> ListarAprobacion()
        {
            try
            {
                AprobacionDA objDA = new AprobacionDA();
                return objDA.ListarAprobacion();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AprobacionBE ObtenerAprobacion(int Id, int Tipo)
        {
            try
            {
                AprobacionDA objDA = new AprobacionDA();
                return objDA.ObtenerAprobacion(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarAprobacion(AprobacionBE objBE)
        {
            try
            {
                AprobacionDA objDA = new AprobacionDA();
                return objDA.InsertarAprobacion(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarAprobacion(AprobacionBE objBE)
        {
            try
            {
                AprobacionDA objDA = new AprobacionDA();
                objDA.ModificarAprobacion(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
