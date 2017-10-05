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
    public class BancoDA
    {
        // Listar Banco
        public List<BancoBE> ListarBanco(int Id, int Tipo, int Tipo2)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;
            SqlParameter pTipo2;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_BancoListar";
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
                
                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pTipo2);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<BancoBE> lstBancoBE;
                BancoBE objBancoBE;
                lstBancoBE = new List<BancoBE>();

                while (sqlDR.Read())
                {
                    objBancoBE = new BancoBE();
                    objBancoBE.IdBanco = sqlDR.GetInt32(sqlDR.GetOrdinal("IdBanco"));
                    objBancoBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objBancoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objBancoBE.Cuenta = sqlDR.GetString(sqlDR.GetOrdinal("Cuenta"));
                    objBancoBE.Moneda = sqlDR.GetString(sqlDR.GetOrdinal("Moneda"));
                    objBancoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objBancoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objBancoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objBancoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstBancoBE.Add(objBancoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstBancoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Banco
        public BancoBE ObtenerBanco(int Id)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdBanco;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_BancoObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdBanco = new SqlParameter();
                pIdBanco.ParameterName = "@Id";
                pIdBanco.SqlDbType = SqlDbType.Int;
                pIdBanco.Value = Id;
                sqlCmd.Parameters.Add(pIdBanco);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                BancoBE objBancoBE;
                objBancoBE = null;

                while (sqlDR.Read())
                {
                    objBancoBE = new BancoBE();
                    objBancoBE.IdBanco = sqlDR.GetInt32(sqlDR.GetOrdinal("IdBanco"));
                    objBancoBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objBancoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objBancoBE.Cuenta = sqlDR.GetString(sqlDR.GetOrdinal("Cuenta"));
                    objBancoBE.Moneda = sqlDR.GetString(sqlDR.GetOrdinal("Moneda"));
                    objBancoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objBancoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objBancoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objBancoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objBancoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
