using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.XPath;
using MSS.TAWA.BE;
using MSS.TAWA.HP;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            var url = "controlaccounts/getcontrolaccounts.xsjs?document=" + tipoDocumentoWeb.GetPrefix() + "&code=" + U_codigo;

            //recover field cuenta contable
            var response = GetJsonResponse(url, null, "U_CuentaContable");
            if (string.IsNullOrEmpty(response))
                throw new Exception("ControlAccount not found. Query: " + url);

            return response;
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

            var url = "controlaccounts/getcontrolaccounts.xsjs?document=" + tipoDocumentoWeb.GetPrefix() + "&code=" + U_codigo;

            //recover field cuenta contable
            var response = GetJsonResponse(url, null, "U_CuentaContable");
            if (string.IsNullOrEmpty(response))
                throw new Exception("ControlAccount not found. Query: " + url);

            return response;
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

            var url = "controlaccounts/getcontrolaccounts.xsjs?document=" + tipoDocumentoWeb.GetPrefix() + "&code=" + U_codigo;

            //recover field cuenta contable
            var response = GetJsonResponse(url, null, "U_CuentaContable");
            if (string.IsNullOrEmpty(response))
                throw new Exception("ControlAccount not found. Query: " + url);

            return response;
        }

        //RENDICIÓN DETALLE
        public string GetAccountCode(string CodigoConceptoSAP)
        {
            //column: U_CuentaContable
            var url = "concepts/getconcepts.xsjs?code=" + CodigoConceptoSAP;

            //recover field cuenta contable
            var response = GetJsonResponse(url, null, "U_CuentaContable");
            if (string.IsNullOrEmpty(response))
                throw new Exception("ControlAccount not found. Query: " + url);

            return response;
        }

        //RENDICIÓN DETALLE
        public string GetAccountCode(string codigoCuenta, bool esDevolucion)
        {
            //get column U_CuentaContable
            var url = "returnaccounts/getreturnaccount.xsjs?code=" + codigoCuenta;

            //recover field cuenta contable
            var response = GetJsonResponse(url, null, "U_CuentaContable");
            if (string.IsNullOrEmpty(response))
                throw new Exception("ControlAccount not found. Query: " + url);

            return response;
        }

        public decimal GetRate(string docCurrency)
        {
            //column:Rate
            var url = "rates/getrate.xsjs?code=" + docCurrency;
            //recover field cuenta contable
            var response = GetJsonResponse(url, null, "Rate");
            if (string.IsNullOrEmpty(response))
                throw new Exception("GetRate not found. Query: " + url);

            return Convert.ToDecimal(response);
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

            //COLUMN: Series
            var url = "series/getseries.xsjs?code=" + prefix;
            var response = GetJsonResponse(url, null, "Series");
            return response;
        }

        public string GetPartidaPresupuestal(string U_MSSP_NIV)
        {
            return string.Empty;
        }


        #region GetJson From Url

        private static string GetUrlPath()
        {
            var ip = ConfigurationManager.AppSettings["XSJSPath"];
            return ip;
        }

        public static dynamic GetJsonResponse(string path, Type deserealizeType = null, string propertyName = null)
        {
            var url = GetUrlPath() + path;
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

            using (var response = webrequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var respuesta = reader.ReadToEnd();
                if ((JsonConvert.DeserializeObject(respuesta) as dynamic).ResponseStatus == "Error")
                    throw new Exception("Internal error: " +
                                        (JsonConvert.DeserializeObject(respuesta) as dynamic).Response.message.value);

                var responseProperty = (JsonConvert.DeserializeObject(respuesta) as dynamic).Response;

                if (deserealizeType != null)
                {
                    if (deserealizeType.IsGenericType && deserealizeType.Name == "List`1")
                    {
                        var itemType = deserealizeType.GetGenericArguments().Single();
                        return ParseJsonToList(responseProperty.ToString(), itemType);
                    }
                    return JsonConvert.DeserializeObject(responseProperty["0"].ToString(), deserealizeType);
                }

                if (propertyName != null)
                {
                    try
                    {
                        return responseProperty["0"][propertyName].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        return responseProperty["0"][propertyName.Replace("U_", "")].ToString();
                    }
                    catch (Exception ex)
                    {
                        return string.Empty;
                    }
                }

                return responseProperty.ToString();
            }
        }

        private static dynamic ParseJsonToList(dynamic jsonString, Type itemType)
        {
            var listType = typeof(List<>).MakeGenericType(new[] { itemType });
            var list = (IList)Activator.CreateInstance(listType);
            var parsedJson = JObject.Parse(jsonString);
            foreach (var jProperty in parsedJson.Properties())
            {
                var item = JsonConvert.DeserializeObject(jProperty.Value.ToString(), itemType);
                list.Add(item);
            }

            return list;
            /*
            string json = "{a: 10, b: 'aaaaaa', c: 1502}";

            var parsedJson = JObject.Parse(json);
            foreach (var jProperty in parsedJson.Properties())
            {
                Console.WriteLine(string.Format("Name: [{0}], Value: [{1}].", jProperty.Name, jProperty.Value));
                list.Add(jProperty.Value);
            }*/
        }

        #endregion
    }
}