using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class EmpresaBC
    {
        public List<EmpresaBE> ListarEmpresa()
        {
            try
            {
                EmpresaDA objDA = new EmpresaDA();
                return objDA.ListarEmpresa();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EmpresaBE ObtenerEmpresa(int Id)
        {
            try
            {
                EmpresaDA objDA = new EmpresaDA();
                return objDA.ObtenerEmpresa(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
