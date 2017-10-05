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
    public class MotivoDA
    {
        // Listar Motivo
        public List<MotivoBE> ListarMotivo(int Id, int Tipo)
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
                strSP = "MSS_WEB_MotivoListar";
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

                List<MotivoBE> lstMotivoBE;
                MotivoBE objMotivoBE;
                lstMotivoBE = new List<MotivoBE>();

                while (sqlDR.Read())
                {
                    objMotivoBE = new MotivoBE();
                    objMotivoBE.IdMotivo = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMotivo"));
                    objMotivoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objMotivoBE.Dias = sqlDR.GetInt32(sqlDR.GetOrdinal("Dias"));
                    objMotivoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objMotivoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objMotivoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objMotivoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstMotivoBE.Add(objMotivoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstMotivoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Motivo
        public MotivoBE ObtenerMotivo(int Id)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdMotivo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_MotivoObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdMotivo = new SqlParameter();
                pIdMotivo.ParameterName = "@IdMotivo";
                pIdMotivo.SqlDbType = SqlDbType.Int;
                pIdMotivo.Value = Id;
                sqlCmd.Parameters.Add(pIdMotivo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                MotivoBE objMotivoBE;
                objMotivoBE = null;

                while (sqlDR.Read())
                {
                    objMotivoBE = new MotivoBE();
                    objMotivoBE.IdMotivo = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMotivo"));
                    objMotivoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objMotivoBE.Dias = sqlDR.GetInt32(sqlDR.GetOrdinal("Dias"));
                    objMotivoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objMotivoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objMotivoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objMotivoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objMotivoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
