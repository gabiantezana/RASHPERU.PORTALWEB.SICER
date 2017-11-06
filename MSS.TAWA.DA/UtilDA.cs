using System;
using System.Configuration;
using MSS.TAWA.HP;

namespace MSS.TAWA.DA
{
    public class UtilDA
    {
        //APERTURA CABECERA
        public string GetControlAccount(TipoDocumentoWeb tipoDocumentoWeb, string docCurrency)
        {
            var U_codigo = string.Empty;
            switch (docCurrency)
            {
                case "SOL":
                case "S/":
                case "SOLES":
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            U_codigo = "DPRMN";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            U_codigo = "ERCTAMN";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            U_codigo = "REDPRMN";
                            break;
                    }
                    break;
                default:
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            U_codigo = "DPRME";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            U_codigo = "ERCTAME";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            U_codigo = "REDPRME";
                            break;
                    }
                    break;
            }

            var queryControlAccount = "EXEC MSS_SP_SICER_GETACCOUNTFROMCONFIG '" + tipoDocumentoWeb.GetPrefix() +
                                      "' , '" + U_codigo + "'";
            var strSP = "MSS_SP_SICER_GETACCOUNTFROMCONFIG";
            var controlAccount = string.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString,
                queryControlAccount);
            while (!rs.EOF)
            {
                controlAccount = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return controlAccount;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

        //RENDICIÓN CABECERA
        public string GetControlAccount(TipoDocumentoWeb tipoDocumentoWeb, string docCurrency, Boolean esRendicion)
        {
            var U_codigo = string.Empty;
            switch (docCurrency)
            {
                case "SOL":
                case "S/":
                case "SOLES":
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            U_codigo = "DPRMN";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            U_codigo = "ERMNI";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            U_codigo = "REDPRMN";
                            break;
                    }
                    break;
                default:
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            U_codigo = "DPRME";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            U_codigo = "ERMEI";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            U_codigo = "REDPRME";
                            break;
                    }
                    break;
            }

            var queryControlAccount = "EXEC MSS_SP_SICER_GETACCOUNTFROMCONFIG '" + tipoDocumentoWeb.GetPrefix() +
                                      "' , '" + U_codigo + "'";
            var strSP = "MSS_SP_SICER_GETACCOUNTFROMCONFIG";
            var controlAccount = string.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString,
                queryControlAccount);
            while (!rs.EOF)
            {
                controlAccount = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return controlAccount;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

        //APERTURA DETALLE
        public string GetAccountCode(TipoDocumentoWeb tipoDocumentoWeb, string docCurrency)
        {
            var U_codigo = string.Empty;
            switch (docCurrency)
            {
                case "SOL":
                case "S/":
                case "SOLES":
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            U_codigo = "FFMN";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            U_codigo = "ERMN";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            U_codigo = "REMN";
                            break;
                    }
                    break;
                default:
                    switch (tipoDocumentoWeb)
                    {
                        case TipoDocumentoWeb.CajaChica:
                            U_codigo = "FFME";
                            break;
                        case TipoDocumentoWeb.EntregaRendir:
                            U_codigo = "ERME";
                            break;
                        case TipoDocumentoWeb.Reembolso:
                            U_codigo = "REME";
                            break;
                    }
                    break;
            }

            var queryControlAccount = "EXEC MSS_SP_SICER_GETACCOUNTFROMCONFIG '" + tipoDocumentoWeb.GetPrefix() +
                                      "' , '" + U_codigo + "'";
            var strSP = "MSS_SP_SICER_GETACCOUNTFROMCONFIG";
            var controlAccount = string.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString,
                queryControlAccount);
            while (!rs.EOF)
            {
                controlAccount = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return controlAccount;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

        //RENDICIÓN DETALLE
        public string GetAccountCode(string CodigoConceptoSAP)
        {
            var strSP = "EXEC MSS_WEB_ConceptoObtener '" + CodigoConceptoSAP + "'";
            var accountCode = string.Empty;

            var rs = new SQLConnectionHelper().DoQuery(ConfigurationManager.ConnectionStrings["SICER"].ConnectionString,
                strSP);
            while (!rs.EOF)
            {
                accountCode = rs.Fields.Item("U_CuentaContable").Value.ToString();
                return accountCode;
            }

            throw new Exception("ControlAccount not found. Query: " + strSP);
        }

        //RENDICIÓN DETALLE
        public string GetAccountCode(string codigoCuenta, bool esDevolucion)
        {
            var strSP = "EXEC MSS_WEB_CuentaContableDevolucionObtener '" + codigoCuenta + "'";
            var ControlAccount = string.Empty;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(
                    ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
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

        public decimal GetRate(string docCurrency)
        {
            var strSP = "EXEC MSS_WEB_TIPOCAMBIO_GET '" + docCurrency + "'";
            decimal Rate = 1;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(
                    ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
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

        public int GetSeries(TipoDocumentoSunat tipoDocumentoSunat)
        {
            var prefix = string.Empty;
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

            var strSP = "EXEC MSS_WEB_GETSERIES '" + prefix + "'";
            var Series = 0;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(
                    ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
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

        public string GetPartidaPresupuestal(string U_MSSP_NIV)
        {
            var strSP = "EXEC MSS_WEB_PARTIDAPRESUPUESTAL_GET '" + U_MSSP_NIV + "'";
            var partidaPresupuestal = string.Empty;
            try
            {
                var rs = new SQLConnectionHelper().DoQuery(
                    ConfigurationManager.ConnectionStrings["SICER"].ConnectionString, strSP);
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