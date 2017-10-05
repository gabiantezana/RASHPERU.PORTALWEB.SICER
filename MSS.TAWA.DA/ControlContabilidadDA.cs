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
    public class ControlContabilidadDA
    {

        public bool InsertarControlContabilidad(ControlContabilidadBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;


            SqlParameter pIdDocumento;
            SqlParameter pCodigoDocumento;
            SqlParameter pUserUpdate;
            SqlParameter pCreateDate;
            SqlParameter pFechaContabilizacion;
            SqlParameter pGetdate;
            SqlParameter pDocumento;
            SqlParameter pHostname;
            SqlParameter pIP;



            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_ControlContabilidadInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdDocumento = new SqlParameter();
                pIdDocumento.ParameterName = "@IdDocumento";
                pIdDocumento.SqlDbType = SqlDbType.Int;
                pIdDocumento.Size = 100;
                pIdDocumento.Value = objBE.IdDocumento;

                pCodigoDocumento = new SqlParameter();
                pCodigoDocumento.ParameterName = "@CodigoDocumento";
                pCodigoDocumento.SqlDbType = SqlDbType.VarChar;
                pCodigoDocumento.Value = objBE.CodigoDocumento;

                pUserUpdate = new SqlParameter();
                pUserUpdate.ParameterName = "@UserUpdate";
                pUserUpdate.SqlDbType = SqlDbType.Int;
                pUserUpdate.Value = objBE.UserUpdate;

                pCreateDate = new SqlParameter();
                pCreateDate.ParameterName = "@CreateDate";
                pCreateDate.SqlDbType = SqlDbType.DateTime;
                pCreateDate.Value = objBE.CreateDate;

                pFechaContabilizacion = new SqlParameter();
                pFechaContabilizacion.ParameterName = "@FechaContabilizacion";
                pFechaContabilizacion.SqlDbType = SqlDbType.DateTime;
                pFechaContabilizacion.Value = objBE.FechaContabilizacion;

                pGetdate = new SqlParameter();
                pGetdate.ParameterName = "@Getdate";
                pGetdate.SqlDbType = SqlDbType.DateTime;
                pGetdate.Value = objBE.Getdate;

                pDocumento = new SqlParameter();
                pDocumento.ParameterName = "@Documento";
                pDocumento.SqlDbType = SqlDbType.VarChar;
                pDocumento.Value = objBE.Documento;

                pHostname = new SqlParameter();
                pHostname.ParameterName = "@Hostname";
                pHostname.SqlDbType = SqlDbType.VarChar;
                pHostname.Value = objBE.Hostname;

                pIP = new SqlParameter();
                pIP.ParameterName = "@IP";
                pIP.SqlDbType = SqlDbType.VarChar;
                pIP.Value = objBE.IP;

                sqlCmd.Parameters.Add(pIdDocumento);
                sqlCmd.Parameters.Add(pCodigoDocumento);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pFechaContabilizacion);
                sqlCmd.Parameters.Add(pGetdate);
                sqlCmd.Parameters.Add(pDocumento);
                sqlCmd.Parameters.Add(pHostname);
                sqlCmd.Parameters.Add(pIP);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();


                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();

                return true;
            }

            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
    }
}
