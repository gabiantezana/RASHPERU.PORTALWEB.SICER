using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class CuentasContablesDevolucionDA
    {
        public List<CuentaContableDevolucionBE> ListarCuentas()
        {
            var url = "returnaccounts/getreturnaccountslist.xsjs";
            var response = UtilDA.GetJsonResponse(url, typeof(List<CuentaContableDevolucionBE>));
            return response;
        }

        public CuentaContableDevolucionBE ObtenerCuentaContable(string codigo)
        {
            var url = "returnaccounts/getreturnaccount.xsjs?code=" + codigo;
            var response = UtilDA.GetJsonResponse(url, typeof(CuentaContableDevolucionBE));
            return response;
        }
    }
}