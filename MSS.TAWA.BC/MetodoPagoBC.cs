using System;
using System.Collections.Generic;
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
                var objDA = new MetodoPagoDA();
                return objDA.ListarMetodoPago(Id, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MetodoPagoBE ObtenerMetodoPago(int Id)
        {
            try
            {
                var objDA = new MetodoPagoDA();
                return objDA.ObtenerMetodoPago(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}