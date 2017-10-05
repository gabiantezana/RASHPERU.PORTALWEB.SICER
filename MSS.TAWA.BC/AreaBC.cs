using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class AreaBC
    {
        public List<AreaBE> ListarArea(int Id, int Tipo)
        {
            try
            {
                AreaDA objDA = new AreaDA();
                return objDA.ListarArea(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AreaBE ObtenerArea(int Id)
        {
            try
            {
                AreaDA objDA = new AreaDA();
                return objDA.ObtenerArea(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
