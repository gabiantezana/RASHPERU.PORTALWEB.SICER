using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnumHelper
/// </summary>
public static class EnumHelper
{
    public static String IdToString(this Enum enumType)
    {
        var asdf = (Int32)Enum.Parse(enumType.GetType(), enumType.ToString());
        return asdf.ToString();
    }
}

public enum Modo
{
    Crear = 1,
    Editar = 2,
}

public enum TipoDocumentoWeb
{
    CajaChica = 1,
    EntregaRendir = 2,
    Reembolso = 3,
}

public enum TipoDocumentoSunat
{
    Facturas,
    Boletas,
    ReciboHonorarios,
    Tickets,
    ReciboInterno,
    PlanillaMovilidad,
    RecibosPublicos,
    NotaCredito,
    NotaDebito,
    Devolucion
}

public enum EstadoDocumento
{
    PorAprobarNivel1 = 1,
    PorAprobarNivel2 = 2,
    PorAprobarNivel3 = 3,
    Aprobado = 4,
    Rechazado = 5,
    ObservacionNivel1 = 8,
    ObservacionNivel2 = 9,
    ObservacionNivel3 = 10,
    //RendirPorAprobarNivel1 = 11,
    //RendirObservacionNivel1 = 12,
    //RendirPorAprobarNivel2 = 13,
    //RendirObservacionNivel2 = 14,
    //RendirPorAprobarNivel3 = 15,
    RendirPorAprobarJefeArea = 11,
    RendirObservacionesNivel1 = 12,
    RendirPorAprobarContabilidad = 13,
    RendirObservacionContabilidad = 14,
    RendirAprobado = 15,
    Liquidado = 16,
}
public enum TipoAprobador
{
    Aprobador = 1,
    Contabilidad = 2,
    Creador = 3,
    AprobadorYCreador = 4,
    ContabilidadYCreador = 5,

}

