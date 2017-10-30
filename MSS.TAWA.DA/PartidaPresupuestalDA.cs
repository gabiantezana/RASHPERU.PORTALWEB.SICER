using MSS.TAWA.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MSS.TAWA.DA
{
    public class PartidaPresupuestalDA
    {
        public List <PartidaPresupuestalBE> GetListadoPartidasPresupuestales(String codigoCentroCostos)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_PARTIDAPRESUPUESTAL_GETLIST";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@CodigoCentroCostos";
                pIdUsuario.SqlDbType = SqlDbType.NVarChar;
                pIdUsuario.Value = codigoCentroCostos;

                sqlCmd.Parameters.Add(pIdUsuario);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<PartidaPresupuestalBE>  list = new List<PartidaPresupuestalBE>();

                while (sqlDR.Read())
                {
                    PartidaPresupuestalBE partida = new PartidaPresupuestalBE();
                    partida.Code = sqlDR.GetString(sqlDR.GetOrdinal("Code"));
                    partida.U_MSSP_NIV = sqlDR.GetString(sqlDR.GetOrdinal("U_MSSP_NIV"));
                    list.Add(partida);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartidaPresupuestalBE GetPartidaPresupuestal(String U_MSSP_NIV)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_PARTIDAPRESUPUESTAL_GET";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@U_MSSP_NIV";
                pIdUsuario.SqlDbType = SqlDbType.NVarChar;
                pIdUsuario.Value = U_MSSP_NIV;

                sqlCmd.Parameters.Add(pIdUsuario);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                PartidaPresupuestalBE partida = new PartidaPresupuestalBE();

                while (sqlDR.Read())
                {
                    partida.Code = sqlDR.GetString(sqlDR.GetOrdinal("Code"));
                    partida.U_MSSP_NIV = sqlDR.GetString(sqlDR.GetOrdinal("U_MSSP_NIV"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return partida;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
