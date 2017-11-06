using System.Collections.Generic;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class CuentaContableBC
    {
        public List<CuentaContableDevolucionBE> GetCuentasContables()
        {
            return new CuentasContablesDevolucionDA().ListarCuentas();
        }

        public CuentaContableDevolucionBE GetCuentaContable(string codigoCuentaContable)
        {
            return new CuentasContablesDevolucionDA().ObtenerCuentaContable(codigoCuentaContable);
        }
    }
}