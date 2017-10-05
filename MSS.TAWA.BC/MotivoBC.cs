using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class MotivoBC
    {
        public List<MotivoBE> ListarMotivo(int Id, int Tipo)
        {
            try
            {
                MotivoDA objDA = new MotivoDA();
                return objDA.ListarMotivo(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MotivoBE ObtenerMotivo(int Id)
        {
            try
            {
                MotivoDA objDA = new MotivoDA();
                return objDA.ObtenerMotivo(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
