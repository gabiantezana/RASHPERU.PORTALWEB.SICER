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
    public class RendicionDA
    {
        // Listar Redicion
        public List<RendicionBE> ListarRedicion(int Id, int Tipo)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_RendicionListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = Id;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pTipo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<RendicionBE> lstRedicionBE;
                RendicionBE objRedicionBE;
                lstRedicionBE = new List<RendicionBE>();

                while (sqlDR.Read())
                {
                    objRedicionBE = new RendicionBE();
                    objRedicionBE.A01 = sqlDR.GetString(sqlDR.GetOrdinal("A01"));
                    objRedicionBE.A02 = sqlDR.GetString(sqlDR.GetOrdinal("A02"));
                    objRedicionBE.A03 = sqlDR.GetString(sqlDR.GetOrdinal("A03"));
                    objRedicionBE.A04 = sqlDR.GetString(sqlDR.GetOrdinal("A04"));
                    objRedicionBE.A05 = sqlDR.GetString(sqlDR.GetOrdinal("A05"));
                    objRedicionBE.A06 = sqlDR.GetString(sqlDR.GetOrdinal("A06"));
                    objRedicionBE.A07 = sqlDR.GetString(sqlDR.GetOrdinal("A07"));
                    objRedicionBE.A08 = sqlDR.GetString(sqlDR.GetOrdinal("A08"));
                    objRedicionBE.A09 = sqlDR.GetString(sqlDR.GetOrdinal("A09"));
                    objRedicionBE.A10 = sqlDR.GetString(sqlDR.GetOrdinal("A10"));
                    objRedicionBE.A11 = sqlDR.GetString(sqlDR.GetOrdinal("A11"));
                    objRedicionBE.A12 = sqlDR.GetString(sqlDR.GetOrdinal("A12"));
                    objRedicionBE.A13 = sqlDR.GetString(sqlDR.GetOrdinal("A13"));
                    objRedicionBE.A14 = sqlDR.GetString(sqlDR.GetOrdinal("A14"));
                    objRedicionBE.A15 = sqlDR.GetString(sqlDR.GetOrdinal("A15"));
                    objRedicionBE.A16 = sqlDR.GetString(sqlDR.GetOrdinal("A16"));
                    objRedicionBE.A17 = sqlDR.GetString(sqlDR.GetOrdinal("A17"));
                    objRedicionBE.A18 = sqlDR.GetString(sqlDR.GetOrdinal("A18"));
                    objRedicionBE.A19 = sqlDR.GetString(sqlDR.GetOrdinal("A19"));
                    objRedicionBE.A20 = sqlDR.GetString(sqlDR.GetOrdinal("A20"));
                    objRedicionBE.A21 = sqlDR.GetString(sqlDR.GetOrdinal("A21"));
                    objRedicionBE.A22 = sqlDR.GetString(sqlDR.GetOrdinal("A22"));
                    objRedicionBE.A23 = sqlDR.GetString(sqlDR.GetOrdinal("A23"));
                    objRedicionBE.A23 = sqlDR.GetString(sqlDR.GetOrdinal("A24"));
                    lstRedicionBE.Add(objRedicionBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstRedicionBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
