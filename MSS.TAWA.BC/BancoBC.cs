using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class BancoBC
    {
        public List<BancoBE> ListarBanco(int Id, int Tipo, int Tipo2)
        {
            try
            {
                BancoDA objDA = new BancoDA();
                return objDA.ListarBanco(Id, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BancoBE ObtenerBanco(int Id)
        {
            try
            {
                BancoDA objDA = new BancoDA();
                return objDA.ObtenerBanco(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
