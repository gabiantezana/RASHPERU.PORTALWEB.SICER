using MSS.TAWA.BE;
using MSS.TAWA.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.TAWA.BC
{
  public  class PartidaPresupuestalBC
    {
        public List<PartidaPresupuestalBE> GetList(String centroCostos)
        {
            return new PartidaPresupuestalDA().GetListadoPartidasPresupuestales(centroCostos);
        }

        public PartidaPresupuestalBE GetPartidaPresupuestal(String U_MSSP_NIV)
        {
            return new PartidaPresupuestalDA().GetPartidaPresupuestal(U_MSSP_NIV);
        }
    }
}
