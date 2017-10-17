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

public enum TipoDocumento
{
    CajaChica = 1,
    EntregaRendir = 2,
    Reembolso = 3,
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
    ObservacionNivel3 = 10

}

public enum TipoAprobador
{
    Aprobador = 1,
    Contabilidad = 2,
    Creador = 3,
    AprobadorYCreador = 4,
    ContabilidadYCreador = 5,

}

