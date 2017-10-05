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
    public class MetodoPagoDA
    {
        // Listar MetodoPago
        public List<MetodoPagoBE> ListarMetodoPago(int Id, int Tipo, int Tipo2)
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
                strSP = "MSS_WEB_MetodoPagoListar";
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

                List<MetodoPagoBE> lstMetodoPagoBE;
                MetodoPagoBE objMetodoPagoBE;
                lstMetodoPagoBE = new List<MetodoPagoBE>();

                while (sqlDR.Read())
                {
                    objMetodoPagoBE = new MetodoPagoBE();
                    objMetodoPagoBE.IdMetodoPago = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMetodoPago"));
                    objMetodoPagoBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objMetodoPagoBE.PayMethCod = sqlDR.GetString(sqlDR.GetOrdinal("PayMethCod"));
                    objMetodoPagoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objMetodoPagoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objMetodoPagoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objMetodoPagoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objMetodoPagoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstMetodoPagoBE.Add(objMetodoPagoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstMetodoPagoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener MetodoPago
        public MetodoPagoBE ObtenerMetodoPago(int Id)
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
                strSP = "MSS_WEB_MetodoPagoObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdBanco = new SqlParameter();
                pIdBanco.ParameterName = "@Id";
                pIdBanco.SqlDbType = SqlDbType.Int;
                pIdBanco.Value = Id;
                sqlCmd.Parameters.Add(pIdBanco);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                MetodoPagoBE objMetodoPagoBE;
                objMetodoPagoBE = null;

                while (sqlDR.Read())
                {
                    objMetodoPagoBE = new MetodoPagoBE();
                    objMetodoPagoBE.IdMetodoPago = sqlDR.GetInt32(sqlDR.GetOrdinal("IdMetodoPago"));
                    objMetodoPagoBE.IdEmpresa = sqlDR.GetInt32(sqlDR.GetOrdinal("IdEmpresa"));
                    objMetodoPagoBE.PayMethCod = sqlDR.GetString(sqlDR.GetOrdinal("PayMethCod"));
                    objMetodoPagoBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objMetodoPagoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objMetodoPagoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objMetodoPagoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objMetodoPagoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objMetodoPagoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
