using MSS.TAWA.BC;
using MSS.TAWA.BE;
using MSS.TAWA.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ValidationHelper
/// </summary>
public class ValidationHelper
{
    public ValidationHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Boolean ProveedorExiste(String nombreProveedor)
    {
        var objProveedorBE = new ProveedorBC().ObtenerProveedor(0, 1, nombreProveedor);
        if (objProveedorBE == null)
            return false;
        else
            return true;
    }
    public Int32 GetIDProveedor(String nombreProveedor)
    {
        var objProveedorBE = new ProveedorBC().ObtenerProveedor(0, 1, nombreProveedor);
        if (objProveedorBE == null)
            return 0;
        else
            return objProveedorBE.IdProveedor;
    }

    public Double? ObtenerMontoMaximoDeDocumento(Int32 idTipoDocumento, String moneda)
    {
        Double? montoMaximo = null;
        Int32 idNivelMoneda = 0;
        switch (moneda)
        {
            case "SOL":
            case "S/":
            case "S/.":
            case "SL":
                idNivelMoneda = 1;
                break;
            default:
                idNivelMoneda = 2;
                break;
        }
        NivelAprobacionBE nivelAprobacion = new NivelAprobacionDA().ObtenerNivelAprobacionV2(idTipoDocumento, idNivelMoneda);
        if (nivelAprobacion != null)
            if (nivelAprobacion.EsDeMonto == "1")
                montoMaximo = Convert.ToDouble(nivelAprobacion.Monto);
        return montoMaximo;
    }

    public Boolean UsuarioPuedeAprobarDocumento(EstadoDocumento estadoDocumento, UsuarioBE usuario)
    {
        switch (estadoDocumento)
        {
            case EstadoDocumento.PorAprobarNivel1:
                if (usuario.IdUsuarioCC1 == usuario.IdUsuario)
                    return true;
                break;
            case EstadoDocumento.PorAprobarNivel2:
                if (usuario.IdUsuarioCC2 == usuario.IdUsuario)
                    return true;
                break;
            case EstadoDocumento.PorAprobarNivel3:
                if (usuario.IdUsuarioCC3 == usuario.IdUsuario)
                    return true;
                break;
        }
        return false;
    }

    public Boolean UsuarioExcedeCantMaxDocumento(TipoDocumento tipoDocumento, Int32 idUsuario)
    {
        UsuarioBE objUsuarioBE = new UsuarioBC().ObtenerUsuario(idUsuario, 0);
        Int32 cantidadActualDocs = 0;
        Int32 cantidadMaximaDocs = 0;

        switch (tipoDocumento)
        {
            case TipoDocumento.CajaChica:
                cantidadActualDocs = new CajaChicaBC().ListarCajaChica(idUsuario, 2, 10, "", "", "", "", "").Count;
                cantidadMaximaDocs = Convert.ToInt32(objUsuarioBE.CantMaxCC);
                break;
            default:
                throw new NotImplementedException();
        }

        if (cantidadActualDocs < cantidadMaximaDocs)
            return false;
        return true;
    }


}