using MSS.TAWA.HP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.TAWA.DA
{
    public class UtilDA
    {
        //Cabecera
        public String GetControlAccount(TipoDocumentoWeb tipoDocumentoWeb, String docCurrency)
        {
            /*--------------------------------------GET CONTROL ACCOUNT/* cabecera--------------------------------------*/
            String U_codigo = String.Empty;
            switch (docCurrency)
            {
                case "SOL":
                case "S/":
                case "SOLES":
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica: U_codigo = "DPRMN"; break;
                        case TipoDocumentoWeb.EntregaRendir: U_codigo = "ERCTAMN"; break;
                        case TipoDocumentoWeb.Reembolso: U_codigo = "REDPRMN"; break;
                    }
                    break;
                default:
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica: U_codigo = "DPRME"; break;
                        case TipoDocumentoWeb.EntregaRendir: U_codigo = "ERCTAME"; break;
                        case TipoDocumentoWeb.Reembolso: U_codigo = "REDPRME"; break;
                    }
                    break;
            }

            String queryControlAccount = "EXEC MSS_SP_SICER_GETACCOUNTFROMCONFIG '" + tipoDocumentoWeb.GetPrefix() + "' , '" + U_codigo + "'";
            String strSP = "MSS_SP_SICER_GETACCOUNTFROMCONFIG";
            String controlAccount = String.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, queryControlAccount);
            while (!rs.EOF)
            {
                controlAccount = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return controlAccount;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

       //cabecera
        public String GetAccountCode(TipoDocumentoWeb tipoDocumentoWeb, String docCurrency)
        {
            /*--------------------------------------GET CONTROL ACCOUNT/* cabecera--------------------------------------*/
            String U_codigo = String.Empty;
            switch (docCurrency)
            {
                case "SOL":
                case "S/":
                case "SOLES":
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica: U_codigo = "FFMN"; break;
                        case TipoDocumentoWeb.EntregaRendir: U_codigo = "ERMN"; break;
                        case TipoDocumentoWeb.Reembolso: U_codigo = "REMN"; break;
                    }
                    break;
                default:
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica: U_codigo = "FFME"; break;
                        case TipoDocumentoWeb.EntregaRendir: U_codigo = "ERME"; break;
                        case TipoDocumentoWeb.Reembolso: U_codigo = "REME"; break;
                    }
                    break;
            }

            String queryControlAccount = "EXEC MSS_SP_SICER_GETACCOUNTFROMCONFIG '" + tipoDocumentoWeb.GetPrefix() + "' , '" + U_codigo + "'";
            String strSP = "MSS_SP_SICER_GETACCOUNTFROMCONFIG";
            String controlAccount = String.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, queryControlAccount);
            while (!rs.EOF)
            {
                controlAccount = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return controlAccount;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

        //Detalle
        public String GetAccountCode(String CodigoConceptoSAP)
        {
            String strSP = "EXEC MSS_WEB_ConceptoObtener '" + CodigoConceptoSAP + "'";
            String accountCode = String.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
            while (!rs.EOF)
            {
                accountCode = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return accountCode;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

        //Detalle
        public String GetAccountCode(String codigoCuenta, Boolean esDevolucion)
        {
            String strSP = "EXEC MSS_WEB_CuentaContableDevolucionObtener '" + codigoCuenta + "'";
            String ControlAccount = String.Empty;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
                while (!rs.EOF)
                {
                    ControlAccount = rs.Fields.Item("U_CuentaContable").Value.ToString();
                    return ControlAccount;
                }

                throw new Exception("Control Account does not found. Query: " + strSP);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Decimal GetRate(String docCurrency)
        {
            String strSP = "EXEC MSS_WEB_TIPOCAMBIO_GET '" + docCurrency + "'";
            Decimal Rate = 1;
            try
            {

                var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
                while (!rs.EOF)
                {
                    Rate = Convert.ToDecimal(rs.Fields.Item("Rate").Value.ToString());
                    return Rate;
                }
                return Rate;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 GetSeries(TipoDocumentoSunat tipoDocumentoSunat)
        {
            String prefix = String.Empty;
            switch (tipoDocumentoSunat)
            {
                case TipoDocumentoSunat.Factura:
                case TipoDocumentoSunat.Boleta:
                case TipoDocumentoSunat.Tickets:
                case TipoDocumentoSunat.ReciboPublicos:
                    prefix = "FP";
                    break;
                case TipoDocumentoSunat.ReciboDeHonorarios:
                    prefix = "RH";
                    break;
                case TipoDocumentoSunat.ReciboInterno:
                case TipoDocumentoSunat.PlanillaMovilidad:
                case TipoDocumentoSunat.Devolucion:
                    prefix = "DI";
                    break;
                case TipoDocumentoSunat.NotaCredito:
                    prefix = "NC";
                    break;
                default:
                    throw new NotImplementedException();
            }

            String strSP = "EXEC MSS_WEB_GETSERIES '" + prefix + "'";
            Int32 Series = 0;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
                while (!rs.EOF)
                {
                    Series = Convert.ToInt32(rs.Fields.Item("Series").Value.ToString());
                    return Series;
                }
                throw new Exception("Series does not found. Query: " + strSP);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      

        public String GetPartidaPresupuestal(String U_MSSP_NIV)
        {
            String strSP = "EXEC MSS_WEB_PARTIDAPRESUPUESTAL_GET '" + U_MSSP_NIV + "'";
            String partidaPresupuestal = String.Empty;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
                while (!rs.EOF)
                {
                    partidaPresupuestal = rs.Fields.Item("U_MSSP_NIV").Value.ToString();
                    return partidaPresupuestal;
                }

                throw new Exception("Partida presupuestal does not found. Query: " + strSP);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
