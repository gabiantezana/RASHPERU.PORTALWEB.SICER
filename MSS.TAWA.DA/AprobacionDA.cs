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
    public class AprobacionDA
    {
        // Listar Aprobacion
        public List<AprobacionBE> ListarAprobacion()
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
                strSP = "MSS_WEB_AprobacionListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<AprobacionBE> lstAprobacionBE;
                AprobacionBE objAprobacionBE;
                lstAprobacionBE = new List<AprobacionBE>();

                while (sqlDR.Read())
                {
                    objAprobacionBE = new AprobacionBE();
                    objAprobacionBE.IdAprobacion = sqlDR.GetInt32(sqlDR.GetOrdinal("IdAprobacion"));
                    objAprobacionBE.IdUsuarioAprobador = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioAprobador"));
                    objAprobacionBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdDocumento"));
                    objAprobacionBE.Tipo = sqlDR.GetString(sqlDR.GetOrdinal("Tipo"));
                    objAprobacionBE.FechaSolicitud = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaSolicitud"));
                    objAprobacionBE.FechaAprobacion = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaAprobacion"));
                    objAprobacionBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objAprobacionBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objAprobacionBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objAprobacionBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objAprobacionBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstAprobacionBE.Add(objAprobacionBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstAprobacionBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Aprobacion
        public AprobacionBE ObtenerAprobacion(int Id, int Tipo)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdNivel;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_AprobacionObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdNivel = new SqlParameter();
                pIdNivel.ParameterName = "@IdNivel";
                pIdNivel.SqlDbType = SqlDbType.Int;
                pIdNivel.Value = Id;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                sqlCmd.Parameters.Add(pIdNivel);
                sqlCmd.Parameters.Add(pTipo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                AprobacionBE objAprobacionBE;
                objAprobacionBE = null;

                while (sqlDR.Read())
                {
                    objAprobacionBE = new AprobacionBE();
                    objAprobacionBE.IdAprobacion = sqlDR.GetInt32(sqlDR.GetOrdinal("IdAprobacion"));
                    objAprobacionBE.IdUsuarioAprobador = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioAprobador"));
                    objAprobacionBE.IdDocumento = sqlDR.GetInt32(sqlDR.GetOrdinal("IdDocumento"));
                    objAprobacionBE.Tipo = sqlDR.GetString(sqlDR.GetOrdinal("Tipo"));
                    objAprobacionBE.FechaSolicitud = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaSolicitud"));
                    objAprobacionBE.FechaAprobacion = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaAprobacion"));
                    objAprobacionBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objAprobacionBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objAprobacionBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objAprobacionBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objAprobacionBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objAprobacionBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar Aprobacion
        public int InsertarAprobacion(AprobacionBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdAprobacion;
            SqlParameter pIdUsuarioAprobador;
            SqlParameter pIdDocumento;
            SqlParameter pTipo;
            SqlParameter pFechaSolicitud;
            SqlParameter pFechaAprobacion;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_AprobacionInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdAprobacion = new SqlParameter();
                pIdAprobacion.Direction = ParameterDirection.ReturnValue;
                pIdAprobacion.SqlDbType = SqlDbType.Int;

                pIdUsuarioAprobador = new SqlParameter();
                pIdUsuarioAprobador.ParameterName = "@IdUsuarioAprobador";
                pIdUsuarioAprobador.SqlDbType = SqlDbType.Int;
                pIdUsuarioAprobador.Value = objBE.IdUsuarioAprobador;

                pIdDocumento = new SqlParameter();
                pIdDocumento.ParameterName = "@IdDocumento";
                pIdDocumento.SqlDbType = SqlDbType.Int;
                pIdDocumento.Value = objBE.IdDocumento;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.VarChar;
                pTipo.Size = 3;
                pTipo.Value = objBE.Tipo;

                pFechaSolicitud = new SqlParameter();
                pFechaSolicitud.ParameterName = "@FechaSolicitud";
                pFechaSolicitud.SqlDbType = SqlDbType.DateTime;
                pFechaSolicitud.Value = objBE.FechaSolicitud;

                pFechaAprobacion = new SqlParameter();
                pFechaAprobacion.ParameterName = "@FechaAprobacion";
                pFechaAprobacion.SqlDbType = SqlDbType.DateTime;
                pFechaAprobacion.Value = objBE.FechaAprobacion;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.VarChar;
                pEstado.Size = 3;
                pEstado.Value = objBE.Estado;

                pUserCreate = new SqlParameter();
                pUserCreate.ParameterName = "@UserCreate";
                pUserCreate.SqlDbType = SqlDbType.VarChar;
                pUserCreate.Size = 20;
                pUserCreate.Value = objBE.UserCreate;

                pCreateDate = new SqlParameter();
                pCreateDate.ParameterName = "@CreateDate";
                pCreateDate.SqlDbType = SqlDbType.DateTime;
                pCreateDate.Value = objBE.CreateDate;

                pUserUpdate = new SqlParameter();
                pUserUpdate.ParameterName = "@UserUpdate";
                pUserUpdate.SqlDbType = SqlDbType.VarChar;
                pUserUpdate.Size = 20;
                pUserUpdate.Value = objBE.UserUpdate;

                pUpdateDate = new SqlParameter();
                pUpdateDate.ParameterName = "@UpdateDate";
                pUpdateDate.SqlDbType = SqlDbType.DateTime;
                pUpdateDate.Value = objBE.UpdateDate;

                sqlCmd.Parameters.Add(pIdAprobacion);
                sqlCmd.Parameters.Add(pIdUsuarioAprobador);
                sqlCmd.Parameters.Add(pIdDocumento);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pFechaSolicitud);
                sqlCmd.Parameters.Add(pFechaAprobacion);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdAprobacion.Value);

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();

                return Id;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Modificar Aprobacion
        public void ModificarAprobacion(AprobacionBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdAprobacion;
            SqlParameter pIdUsuarioAprobador;
            SqlParameter pIdDocumento;
            SqlParameter pTipo;
            SqlParameter pFechaSolicitud;
            SqlParameter pFechaAprobacion;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_AprobacionModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdAprobacion = new SqlParameter();
                pIdAprobacion.ParameterName = "@IdAprobacion";
                pIdAprobacion.SqlDbType = SqlDbType.Int;
                pIdAprobacion.Value = objBE.IdAprobacion;

                pIdUsuarioAprobador = new SqlParameter();
                pIdUsuarioAprobador.ParameterName = "@IdUsuarioAprobador";
                pIdUsuarioAprobador.SqlDbType = SqlDbType.Int;
                pIdUsuarioAprobador.Value = objBE.IdUsuarioAprobador;

                pIdDocumento = new SqlParameter();
                pIdDocumento.ParameterName = "@IdDocumento";
                pIdDocumento.SqlDbType = SqlDbType.Int;
                pIdDocumento.Value = objBE.IdDocumento;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.VarChar;
                pTipo.Size = 3;
                pTipo.Value = objBE.Tipo;

                pFechaSolicitud = new SqlParameter();
                pFechaSolicitud.ParameterName = "@FechaSolicitud";
                pFechaSolicitud.SqlDbType = SqlDbType.DateTime;
                pFechaSolicitud.Value = objBE.FechaSolicitud;

                pFechaAprobacion = new SqlParameter();
                pFechaAprobacion.ParameterName = "@FechaAprobacion";
                pFechaAprobacion.SqlDbType = SqlDbType.DateTime;
                pFechaAprobacion.Value = objBE.FechaAprobacion;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.VarChar;
                pEstado.Size = 3;
                pEstado.Value = objBE.Estado;

                pUserCreate = new SqlParameter();
                pUserCreate.ParameterName = "@UserCreate";
                pUserCreate.SqlDbType = SqlDbType.VarChar;
                pUserCreate.Size = 20;
                pUserCreate.Value = objBE.UserCreate;

                pCreateDate = new SqlParameter();
                pCreateDate.ParameterName = "@CreateDate";
                pCreateDate.SqlDbType = SqlDbType.DateTime;
                pCreateDate.Value = objBE.CreateDate;

                pUserUpdate = new SqlParameter();
                pUserUpdate.ParameterName = "@UserUpdate";
                pUserUpdate.SqlDbType = SqlDbType.VarChar;
                pUserUpdate.Size = 20;
                pUserUpdate.Value = objBE.UserUpdate;

                pUpdateDate = new SqlParameter();
                pUpdateDate.ParameterName = "@UpdateDate";
                pUpdateDate.SqlDbType = SqlDbType.DateTime;
                pUpdateDate.Value = objBE.UpdateDate;

                sqlCmd.Parameters.Add(pIdAprobacion);
                sqlCmd.Parameters.Add(pIdUsuarioAprobador);
                sqlCmd.Parameters.Add(pIdDocumento);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pFechaSolicitud);
                sqlCmd.Parameters.Add(pFechaAprobacion);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
