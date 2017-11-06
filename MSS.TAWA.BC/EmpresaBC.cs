using System;
using System.Collections.Generic;
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
                var objDA = new EmpresaDA();
                return objDA.ListarEmpresa();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public EmpresaBE ObtenerEmpresa(int Id)
        {
            try
            {
                var objDA = new EmpresaDA();
                return objDA.ObtenerEmpresa(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}