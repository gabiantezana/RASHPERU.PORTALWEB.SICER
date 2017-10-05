using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class MetodoPagoBC
    {
        public List<MetodoPagoBE> ListarMetodoPago(int Id, int Tipo, int Tipo2)
        {
            try
            {
                MetodoPagoDA objDA = new MetodoPagoDA();
                return objDA.ListarMetodoPago(Id, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MetodoPagoBE ObtenerMetodoPago(int Id)
        {
            try
            {
                MetodoPagoDA objDA = new MetodoPagoDA();
                return objDA.ObtenerMetodoPago(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
