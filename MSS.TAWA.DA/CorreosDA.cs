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
    public class CorreosDA
    {
        // Listar Area
        public List<CorreosBE> ObtenerCorreos(int Id)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_CorreosObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = Id;

                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<CorreosBE> lstCorreosBE;
                CorreosBE objCorreosBE;
                lstCorreosBE = new List<CorreosBE>();

                while (sqlDR.Read())
                {
                    objCorreosBE = new CorreosBE();
                    objCorreosBE.Id = sqlDR.GetInt32(sqlDR.GetOrdinal("Id"));
                    objCorreosBE.Correo = sqlDR.GetString(sqlDR.GetOrdinal("Correo"));
                    objCorreosBE.TextoCorreo = sqlDR.GetString(sqlDR.GetOrdinal("TextoCorreo"));
                   
                    lstCorreosBE.Add(objCorreosBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstCorreosBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
