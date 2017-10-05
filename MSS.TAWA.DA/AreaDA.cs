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
    public class AreaDA
    {
        // Listar Area
        public List<AreaBE> ListarArea(int Id, int Tipo)
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
                strSP = "MSS_WEB_AreaListar";
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

                List<AreaBE> lstAreaBE;
                AreaBE objAreaBE;
                lstAreaBE = new List<AreaBE>();

                while (sqlDR.Read())
                {
                    objAreaBE = new AreaBE();
                    objAreaBE.IdArea = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea"));
                    objAreaBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objAreaBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objAreaBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objAreaBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objAreaBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstAreaBE.Add(objAreaBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstAreaBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Area
        public AreaBE ObtenerArea(int Id)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;
            
            SqlParameter pIdArea;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_AreaObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdArea";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = Id;
                sqlCmd.Parameters.Add(pIdArea);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                AreaBE objAreaBE;
                objAreaBE = null;

                while (sqlDR.Read())
                {
                    objAreaBE = new AreaBE();
                    objAreaBE.IdArea = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea"));
                    objAreaBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objAreaBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objAreaBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objAreaBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objAreaBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objAreaBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
