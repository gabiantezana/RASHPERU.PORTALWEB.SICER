using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;
//using System.ServiceProcess;
//using System.Diagnostics;

namespace MSS.TAWA.BC
{
    public class EstadoBC
    {
        public List<EstadoBE> ListarEstado(int EstadoCode)
        {
            try
            {
                EstadoDA objDA = new EstadoDA();
                return objDA.ListarEstado(EstadoCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EstadoBE> ListarCajaChica()
        {
            try
            {
                EstadoDA objDA = new EstadoDA();
                return objDA.EstadoListarCajaChica();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<EstadoBE> ListarEntregaRendir()
        {
            try
            {
                EstadoDA objDA = new EstadoDA();
                return objDA.EstadoListarEntregaRendir();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<EstadoBE> ListarReembolso()
        {
            try
            {
                EstadoDA objDA = new EstadoDA();
                return objDA.EstadoListarReembolso();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
