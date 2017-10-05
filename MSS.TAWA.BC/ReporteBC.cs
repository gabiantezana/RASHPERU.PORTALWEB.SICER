using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class ReporteBC
    {
        public List<ReporteBE> ListarReporte(
               String Nombre_Solicitante,
               String CentroCostos3,
               String CentroCostos4,
               String CentroCostos5,
               String EsFacturable,
               String FechaSolicitudIni,
               String FechaSolicitudFin,
               String Estado,
               String Documento,
               String Empresa,
               String Tipo,
               String Tipo2,
               String Tipo3)
        {
            try
            {
                ReporteDA objDA = new ReporteDA();
                return objDA.ListarReporte(Nombre_Solicitante, CentroCostos3, CentroCostos4, CentroCostos5, EsFacturable, FechaSolicitudIni, FechaSolicitudFin, Estado, Documento, Empresa, Tipo, Tipo2, Tipo3);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
