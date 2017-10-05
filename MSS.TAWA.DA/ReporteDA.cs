using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MSS.TAWA.DA
{
    public class ReporteDA
    {
        // Listar Reporte
        public List<ReporteBE> ListarReporte(
               String Nombre_Solicitante,
	           String CentroCostos3,
	           String CentroCostos4,
	           String CentroCostos5,
               String EsFacturable,
               String FechaSolicitudIni,
               String FechaSolicitudFin,
	           String Estado,
	           String Documento,
               String Empresa,
	           String Tipo,
	           String Tipo2,
	           String Tipo3)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pNombre_Solicitante;
	        SqlParameter pCentroCostos3;
	        SqlParameter pCentroCostos4;
	        SqlParameter pCentroCostos5;
            SqlParameter pEsFacturable;
            SqlParameter pFechaSolicitudIni;
            SqlParameter pFechaSolicitudFin;
            SqlParameter pEstado;
            SqlParameter pDocumento;
            SqlParameter pEmpresa;
	        SqlParameter pTipo;
	        SqlParameter pTipo2;
	        SqlParameter pTipo3;


            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_Reporte";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 200;

                pNombre_Solicitante = new SqlParameter();
                pNombre_Solicitante.ParameterName = "@Nombre_Solicitante";
                pNombre_Solicitante.SqlDbType = SqlDbType.VarChar;
                pNombre_Solicitante.Size = 100;
                pNombre_Solicitante.Value = Nombre_Solicitante;

                pCentroCostos3 = new SqlParameter();
                pCentroCostos3.ParameterName = "@CentroCostos3";
                pCentroCostos3.SqlDbType = SqlDbType.VarChar;
                pCentroCostos3.Size = 100;
                pCentroCostos3.Value = CentroCostos3;

                pCentroCostos4 = new SqlParameter();
                pCentroCostos4.ParameterName = "@CentroCostos4";
                pCentroCostos4.SqlDbType = SqlDbType.VarChar;
                pCentroCostos4.Size = 100;
                pCentroCostos4.Value = CentroCostos4;

                pCentroCostos5 = new SqlParameter();
                pCentroCostos5.ParameterName = "@CentroCostos5";
                pCentroCostos5.SqlDbType = SqlDbType.VarChar;
                pCentroCostos5.Size = 100;
                pCentroCostos5.Value = CentroCostos5;

                pEsFacturable = new SqlParameter();
                pEsFacturable.ParameterName = "@EsFacturable";
                pEsFacturable.SqlDbType = SqlDbType.VarChar;
                pEsFacturable.Size = 100;
                pEsFacturable.Value = EsFacturable;

                pFechaSolicitudIni = new SqlParameter();
                pFechaSolicitudIni.ParameterName = "@FechaSolicitudIni";
                pFechaSolicitudIni.SqlDbType = SqlDbType.VarChar;
                pFechaSolicitudIni.Size = 100;
                pFechaSolicitudIni.Value = FechaSolicitudIni;

                pFechaSolicitudFin = new SqlParameter();
                pFechaSolicitudFin.ParameterName = "@FechaSolicitudFin";
                pFechaSolicitudFin.SqlDbType = SqlDbType.VarChar;
                pFechaSolicitudFin.Size = 100;
                pFechaSolicitudFin.Value = FechaSolicitudFin;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.VarChar;
                pEstado.Size = 100;
                pEstado.Value = Estado;

                pDocumento = new SqlParameter();
                pDocumento.ParameterName = "@Documento";
                pDocumento.SqlDbType = SqlDbType.VarChar;
                pDocumento.Size = 100;
                pDocumento.Value = Documento;

                pEmpresa = new SqlParameter();
                pEmpresa.ParameterName = "@Empresa";
                pEmpresa.SqlDbType = SqlDbType.VarChar;
                pEmpresa.Size = 100;
                pEmpresa.Value = Empresa;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.VarChar;
                pTipo.Size = 100;
                pTipo.Value = Tipo;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.VarChar;
                pTipo2.Size = 100;
                pTipo2.Value = Tipo2;

                pTipo3 = new SqlParameter();
                pTipo3.ParameterName = "@Tipo3";
                pTipo3.SqlDbType = SqlDbType.VarChar;
                pTipo3.Size = 100;
                pTipo3.Value = Tipo3;

                sqlCmd.Parameters.Add(pNombre_Solicitante);
                sqlCmd.Parameters.Add(pCentroCostos3);
                sqlCmd.Parameters.Add(pCentroCostos4);
                sqlCmd.Parameters.Add(pCentroCostos5);
                sqlCmd.Parameters.Add(pEsFacturable);
                sqlCmd.Parameters.Add(pFechaSolicitudIni);
                sqlCmd.Parameters.Add(pFechaSolicitudFin);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pDocumento);
                sqlCmd.Parameters.Add(pEmpresa);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pTipo2);
                sqlCmd.Parameters.Add(pTipo3);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<ReporteBE> lstReporteBE;
                ReporteBE objReporteBE;
                lstReporteBE = new List<ReporteBE>();

                while (sqlDR.Read())
                {
                    objReporteBE = new ReporteBE();
                    objReporteBE.DNI_Creador = sqlDR.GetString(sqlDR.GetOrdinal("DNI_Creador"));
                    objReporteBE.Nombre_Creador = sqlDR.GetString(sqlDR.GetOrdinal("Nombre_Creador"));
                    objReporteBE.DNI_Solicitante = sqlDR.GetString(sqlDR.GetOrdinal("DNI_Solicitante"));
                    objReporteBE.Nombre_Solicitante = sqlDR.GetString(sqlDR.GetOrdinal("Nombre_Solicitante"));
                    objReporteBE.Empresa = sqlDR.GetString(sqlDR.GetOrdinal("Empresa"));
                    objReporteBE.CentroCostos3 = sqlDR.GetString(sqlDR.GetOrdinal("CentroCostos3"));
                    objReporteBE.CentroCostos4 = sqlDR.GetString(sqlDR.GetOrdinal("CentroCostos4"));
                    objReporteBE.CentroCostos5 = sqlDR.GetString(sqlDR.GetOrdinal("CentroCostos5"));
                    objReporteBE.Numero = sqlDR.GetString(sqlDR.GetOrdinal("Numero"));
                    objReporteBE.EsFacturable = sqlDR.GetString(sqlDR.GetOrdinal("EsFacturable"));
                    objReporteBE.MomentoFacturable = sqlDR.GetString(sqlDR.GetOrdinal("MomentoFacturable"));
                    objReporteBE.Moneda = sqlDR.GetString(sqlDR.GetOrdinal("Moneda"));
                    objReporteBE.Importe_Inicial = sqlDR.GetString(sqlDR.GetOrdinal("Importe_Inicial"));
                    objReporteBE.FechaSolicitud = sqlDR.GetString(sqlDR.GetOrdinal("FechaSolicitud"));
                    objReporteBE.FechaAprobacionN1 = sqlDR.GetString(sqlDR.GetOrdinal("FechaAprobacionN1"));
                    objReporteBE.FechaAprobacionN2 = sqlDR.GetString(sqlDR.GetOrdinal("FechaAprobacionN2"));
                    objReporteBE.FechaAprobacionN3 = sqlDR.GetString(sqlDR.GetOrdinal("FechaAprobacionN3"));
                    objReporteBE.Motivo = sqlDR.GetString(sqlDR.GetOrdinal("Motivo"));
                    objReporteBE.Asunto = sqlDR.GetString(sqlDR.GetOrdinal("Asunto"));
                    objReporteBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objReporteBE.MontoNoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoNoAfecto"));
                    objReporteBE.MontoAfecto = sqlDR.GetString(sqlDR.GetOrdinal("MontoAfecto"));
                    objReporteBE.MontoIGV = sqlDR.GetString(sqlDR.GetOrdinal("MontoIGV"));
                    objReporteBE.Importe_Rendido = sqlDR.GetString(sqlDR.GetOrdinal("Importe_Rendido"));
                    objReporteBE.Aprobador_Nivel_1 = sqlDR.GetString(sqlDR.GetOrdinal("Aprobador_Nivel_1"));
                    objReporteBE.Aprobador_Nivel_2 = sqlDR.GetString(sqlDR.GetOrdinal("Aprobador_Nivel_2"));
                    objReporteBE.Aprobador_Nivel_3 = sqlDR.GetString(sqlDR.GetOrdinal("Aprobador_Nivel_3"));
                    lstReporteBE.Add(objReporteBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstReporteBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
