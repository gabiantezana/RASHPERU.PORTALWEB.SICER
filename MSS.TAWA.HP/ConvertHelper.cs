using ADODB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConvertHelper
/// </summary>
public static class ConvertHelper
{
    public static Field Item(this Fields fields, String item)
    {
        return fields[item];
    }

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
            case TipoDocumentoSunat.Factura:
                return "01";
            case TipoDocumentoSunat.Boleta:
                return "02";
            case TipoDocumentoSunat.ReciboDeHonorarios:
                return "03";
            case TipoDocumentoSunat.Tickets:
                return "12";
            case TipoDocumentoSunat.ReciboInterno:
                return "DI";
            case TipoDocumentoSunat.PlanillaMovilidad:
                return "PM";
            case TipoDocumentoSunat.ReciboPublicos:
                return "14";
            case TipoDocumentoSunat.NotaCredito:
                return "07";
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