using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class RendicionBC
    {
        public List<RendicionBE> ListarRedicion(int Id, int Tipo)
        {
            try
            {
                RendicionDA objDA = new RendicionDA();
                return objDA.ListarRedicion(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
