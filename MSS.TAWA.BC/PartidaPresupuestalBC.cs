using System.Collections.Generic;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class PartidaPresupuestalBC
    {
        public List<PartidaPresupuestalBE> GetList(string centroCostos)
        {
            return new PartidaPresupuestalDA().GetListadoPartidasPresupuestales(centroCostos);
        }

        public PartidaPresupuestalBE GetPartidaPresupuestal(string U_MSSP_NIV)
        {
            return new PartidaPresupuestalDA().GetPartidaPresupuestal(U_MSSP_NIV);
        }
    }
}