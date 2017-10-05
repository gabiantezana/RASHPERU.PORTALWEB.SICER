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
    public class EstadoDA
    {

        // Listar Usuario
        public List<EstadoBE> ListarEstado(int EstadoCode)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pEstadoCode;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_EstadoListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pEstadoCode = new SqlParameter();
                pEstadoCode.ParameterName = "@EstadoCode";
                pEstadoCode.SqlDbType = SqlDbType.Int;
                pEstadoCode.Value = EstadoCode;


                sqlCmd.Parameters.Add(pEstadoCode);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<EstadoBE> lstEstadoBE;
                EstadoBE objEstadoBE;
                lstEstadoBE = new List<EstadoBE>();

                while (sqlDR.Read())
                {
                    objEstadoBE = new EstadoBE();
                    objEstadoBE.EstadoCode = sqlDR.GetInt32(sqlDR.GetOrdinal("EstadoCode"));
                    objEstadoBE.EstadoNombre = sqlDR.GetString(sqlDR.GetOrdinal("EstadoNombre"));
                    objEstadoBE.EstadoDescripcion = sqlDR.GetString(sqlDR.GetOrdinal("EstadoDescripcion"));
                    lstEstadoBE.Add(objEstadoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstEstadoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstadoBE> EstadoListarCajaChica()
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
                strSP = "MSS_WEB_EstadoListarCajaChica";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<EstadoBE> lstEstadoBE;
                EstadoBE objEstadoBE;
                lstEstadoBE = new List<EstadoBE>();

                while (sqlDR.Read())
                {
                    objEstadoBE = new EstadoBE();
                    objEstadoBE.EstadoCode = sqlDR.GetInt32(sqlDR.GetOrdinal("EstadoCode"));
                    objEstadoBE.EstadoNombre = sqlDR.GetString(sqlDR.GetOrdinal("EstadoNombre"));
                    lstEstadoBE.Add(objEstadoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstEstadoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstadoBE> EstadoListarEntregaRendir()
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
                strSP = "MSS_WEB_EstadoListarEntregaRendir";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<EstadoBE> lstEstadoBE;
                EstadoBE objEstadoBE;
                lstEstadoBE = new List<EstadoBE>();

                while (sqlDR.Read())
                {
                    objEstadoBE = new EstadoBE();
                    objEstadoBE.EstadoCode = sqlDR.GetInt32(sqlDR.GetOrdinal("EstadoCode"));
                    objEstadoBE.EstadoNombre = sqlDR.GetString(sqlDR.GetOrdinal("EstadoNombre"));
                    lstEstadoBE.Add(objEstadoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstEstadoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstadoBE> EstadoListarReembolso()
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
                strSP = "MSS_WEB_EstadoListarReembolso";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<EstadoBE> lstEstadoBE;
                EstadoBE objEstadoBE;
                lstEstadoBE = new List<EstadoBE>();

                while (sqlDR.Read())
                {
                    objEstadoBE = new EstadoBE();
                    objEstadoBE.EstadoCode = sqlDR.GetInt32(sqlDR.GetOrdinal("EstadoCode"));
                    objEstadoBE.EstadoNombre = sqlDR.GetString(sqlDR.GetOrdinal("EstadoNombre"));
                    lstEstadoBE.Add(objEstadoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstEstadoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        


    }
}
