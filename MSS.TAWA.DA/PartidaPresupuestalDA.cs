using MSS.TAWA.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MSS.TAWA.DA
{
    public class PartidaPresupuestalDA
    {
        [Obsolete]
        public List <PartidaPresupuestalBE> GetListadoPartidasPresupuestales(String codigoCentroCostos)
        {
            return new List<PartidaPresupuestalBE>();
        }

        [Obsolete]
        public PartidaPresupuestalBE GetPartidaPresupuestal(String codigoPartidaPresupuestal)
        {
            return new PartidaPresupuestalBE();
        }
    }
}
