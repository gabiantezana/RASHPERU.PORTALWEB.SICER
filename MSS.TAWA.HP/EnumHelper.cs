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

public enum IGVCode
{
    //IIMP
    IGV = 1,
    IGV_EXO = 2
}

public enum TipoDocumentoSunat
{
    Factura = 2,
    Boleta = 3,
    ReciboDeHonorarios = 4,
    Tickets = 5,
    ReciboInterno = 6,
    PlanillaMovilidad = 7,
    ReciboPublicos = 8,
    NotaCredito = 9,
    Devolucion = 11,

}

public enum EstadoDocumento
{
    PorAprobarNivel1 = 1,
    PorAprobarNivel2 = 2,
    PorAprobarNivel3 = 3,
    Aprobado = 4,
    Rechazado = 5,
    //ObservacionNivel1 = 8,
    //ObservacionNivel2 = 9,
    //ObservacionNivel3 = 10,
    //RendirPorAprobarNivel1 = 11,
    //RendirObservacionNivel1 = 12,
    //RendirPorAprobarNivel2 = 13,
    //RendirObservacionNivel2 = 14,
    //RendirPorAprobarNivel3 = 15,
    RendirPorAprobarJefeArea = 11,
    //RendirObservacionesNivel1 = 12,
    RendirPorAprobarContabilidad = 13,
    //RendirObservacionContabilidad = 14,
    RendirAprobado = 15,
    Liquidado = 16,
}

public enum EstadoDocumentoRendicion
{
    Guardado = 0,
    Rendido = 1,
    Rechazado = 2,
}

public enum EstadoMigracion
{
    Ninguno = 0,
    PendienteDeMigracion = 1,
    MigradoCorrectamente = 2,
    MigradoConErrores = 3
}

public enum TipoAprobador
{
    Aprobador = 1,
    Contabilidad = 2,
    Creador = 3,
    AprobadorYCreador = 4,
    ContabilidadYCreador = 5,

}
public enum PerfilUsuario
{
    AdministradorWeb = 1,
    ContabilidadyCreador_CC_ER_RE = 2,
    AprobadoryCreador_CC_ER_RE = 3,
    Creador_CC_ER_RE = 4,
    Contabilidad = 1002,
    Aprobador_CC_ER_RE = 1003,
    Creador_CC = 1004,
    Creador_ER = 1005,
    Creador_RE = 1006,
    Creador_ER_RE = 1007,
    ContabilidadyCreador_ER_RE = 1008,
    AprobadoryCreador_ER_RE = 1009,
    Sistemas = 1010,
}

public enum DocSubTypeSAP
{
    oPurchaseInvoices = 18,
    oPurchaseCreditNotes = 19,
}

public enum EmpresaInterna
{
    IIMP = 0,
    RASH = 1,
    DESARROLLADORA = 2,

}
