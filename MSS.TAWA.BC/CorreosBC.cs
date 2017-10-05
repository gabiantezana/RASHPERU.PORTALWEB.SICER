using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class CorreosBC
    {
        public List<CorreosBE> ObtenerCorreos(int Id)
        {
            try
            {
                CorreosDA objDA = new CorreosDA();
                return objDA.ObtenerCorreos(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
