using MSS.TAWA.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.TAWA.BE
{
    public class UtilBE
    {
        public List<MODEL.EmpresasSICER> GetEmpresasSICER()
        {
            return new SICER_WEBEntities1().EmpresasSICER.ToList();
        }

    }
}
