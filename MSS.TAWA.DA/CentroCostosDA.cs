using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using MSS.TAWA.BE;
using Newtonsoft.Json;

namespace MSS.TAWA.DA
{

    public class CentroCostosDA
    {

        // Listar CentroCostosNivel5
        public List<CentroCostosBE> ListarCentroCostos(int Nivel)
        {
            var url = "costcenter/getcostcenterlist.xsjs?level=" + Nivel;
            var response = UtilDA.GetJsonResponse(url, typeof(List<CentroCostosBE>));
            return response;
        }

        // Obtener CentroCostosNivel5
        public CentroCostosBE ObtenerCentroCostos(string CodigoSAP)
        {
            var url = "costcenter/getcostcenter.xsjs?code=" + CodigoSAP;
            var response = UtilDA.GetJsonResponse(url,typeof(CentroCostosBE));
            return response;
        }
    }
}