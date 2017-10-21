using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConvertHelper
/// </summary>
public static class ConvertHelper
{
    public static Boolean IsNumeric(this String value)
    {
        Decimal decimalParsed = 0;
        if (Decimal.TryParse(value ?? String.Empty, out decimalParsed))
            return true;
        else
            return false;
    }

    public static String GetPrefix(this TipoDocumentoWeb tipoDocumento)
    {
        switch (tipoDocumento)
        {
            case TipoDocumentoWeb.CajaChica:
                return "CC";
            case TipoDocumentoWeb.EntregaRendir:
                return "ER";
            case TipoDocumentoWeb.Reembolso:
                return "RE";
            default:
                throw new NotImplementedException();
        }
    }

    public static String GetPrefix(this TipoDocumentoSunat tipoDocumentoSunat)
    {
        switch (tipoDocumentoSunat)
        {
            case TipoDocumentoSunat.Facturas:
                return "01";
            case TipoDocumentoSunat.Boletas:
                return "02";
            case TipoDocumentoSunat.ReciboHonorarios:
                return "02";
            case TipoDocumentoSunat.Tickets:
                return "";
            case TipoDocumentoSunat.ReciboInterno:
                return "DI";
            case TipoDocumentoSunat.PlanillaMovilidad:
                return "PM";
            case TipoDocumentoSunat.RecibosPublicos:
                return "";
            case TipoDocumentoSunat.NotaCredito:
                return "07";
            case TipoDocumentoSunat.NotaDebito:
                return "08";
            case TipoDocumentoSunat.Devolucion:
                return "DV";
            default:
                throw new NotImplementedException();
        }
    }

    public static String GetName(this TipoDocumentoWeb tipoDocumentoWeb)
    {
        switch (tipoDocumentoWeb)
        {
            case TipoDocumentoWeb.CajaChica:
                return "Caja Chica";
            case TipoDocumentoWeb.EntregaRendir:
                return "Entrega a Rendir";
            case TipoDocumentoWeb.Reembolso:
                return "Reembolso";
            default:
                throw new NotImplementedException();
        }
    }
}