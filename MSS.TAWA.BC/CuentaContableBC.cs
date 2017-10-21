using MSS.TAWA.BE;
using MSS.TAWA.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BC
{
    public class CuentaContableBC
    {
        public List <CuentaContableDevolucionBE> GetCuentasContables()
        {
            return new CuentasContablesDevolucionDA().ListarCuentas();
        }

        public CuentaContableDevolucionBE GetCuentaContable(String codigoCuentaContable)
        {
            return new CuentasContablesDevolucionDA().ObtenerCuentaContable(codigoCuentaContable);
        }
    }
}
