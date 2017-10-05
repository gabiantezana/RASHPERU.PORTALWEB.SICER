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
    public class EmpresaDA
    {
        // Listar Empresa
        public List<EmpresaBE> ListarEmpresa()
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
                strSP = "MSS_WEB_EmpresaListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<EmpresaBE> lstEmpresaBE;
                EmpresaBE objEmpresaBE;
                lstEmpresaBE = new List<EmpresaBE>();

                while (sqlDR.Read())
                {
                    objEmpresaBE = new EmpresaBE();
                    objEmpresaBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objEmpresaBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objEmpresaBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objEmpresaBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objEmpresaBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objEmpresaBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    objEmpresaBE.MontoRedondeo = sqlDR.GetDecimal(sqlDR.GetOrdinal("MontoRedondeo"));
                    lstEmpresaBE.Add(objEmpresaBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstEmpresaBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Empresa
        public EmpresaBE ObtenerEmpresa(int Id)
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
                strSP = "MSS_WEB_EmpresaObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdEmpresa";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = Id;
                sqlCmd.Parameters.Add(pIdArea);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                EmpresaBE objEmpresaBE;
                objEmpresaBE = null;

                while (sqlDR.Read())
                {
                    objEmpresaBE = new EmpresaBE();
                    objEmpresaBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objEmpresaBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objEmpresaBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objEmpresaBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objEmpresaBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objEmpresaBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    objEmpresaBE.MontoRedondeo = sqlDR.GetDecimal(sqlDR.GetOrdinal("MontoRedondeo"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objEmpresaBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
