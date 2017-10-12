using MSS.TAWA.BC;
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

}