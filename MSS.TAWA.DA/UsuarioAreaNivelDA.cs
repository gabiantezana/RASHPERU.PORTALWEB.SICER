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
    public class UsuarioAreaNivelDA
    {
        // Listar NivelAprobacion
        public List<UsuarioAreaNivelBE> ListarUsuarioAreaNivel(int IdUsuario, int Tipo, int Tipo2)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdUsuario;
            SqlParameter pTipo;
            SqlParameter pTipo2;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioAreaNivelListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = IdUsuario;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pTipo2);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<UsuarioAreaNivelBE> lstUsuarioAreaNivelBE;
                UsuarioAreaNivelBE objUsuarioAreaNivelBE;
                lstUsuarioAreaNivelBE = new List<UsuarioAreaNivelBE>();

                while (sqlDR.Read())
                {
                    objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
                    objUsuarioAreaNivelBE.Id = sqlDR.GetInt32(sqlDR.GetOrdinal("Id"));
                    objUsuarioAreaNivelBE.IdUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuario"));
                    objUsuarioAreaNivelBE.IdArea = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea"));
                    objUsuarioAreaNivelBE.IdNivelAprobacion = sqlDR.GetInt32(sqlDR.GetOrdinal("IdNivelAprobacion"));
                    objUsuarioAreaNivelBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objUsuarioAreaNivelBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objUsuarioAreaNivelBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objUsuarioAreaNivelBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstUsuarioAreaNivelBE.Add(objUsuarioAreaNivelBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstUsuarioAreaNivelBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener NivelAprobacion
        public UsuarioAreaNivelBE ObtenerUsuarioAreaNivel(int IdUsuario, int Tipo, int Tipo2)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdUsuario;
            SqlParameter pTipo;
            SqlParameter pTipo2;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioAreaNivelObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = IdUsuario;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pTipo2);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                UsuarioAreaNivelBE objUsuarioAreaNivelBE;
                objUsuarioAreaNivelBE = null;

                while (sqlDR.Read())
                {
                    objUsuarioAreaNivelBE = new UsuarioAreaNivelBE();
                    objUsuarioAreaNivelBE.Id = sqlDR.GetInt32(sqlDR.GetOrdinal("Id"));
                    objUsuarioAreaNivelBE.IdUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuario"));
                    objUsuarioAreaNivelBE.IdArea = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea"));
                    objUsuarioAreaNivelBE.IdNivelAprobacion = sqlDR.GetInt32(sqlDR.GetOrdinal("IdNivelAprobacion"));
                    objUsuarioAreaNivelBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objUsuarioAreaNivelBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objUsuarioAreaNivelBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objUsuarioAreaNivelBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objUsuarioAreaNivelBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar UsuarioAreaNivel
        public int InsertarUsuarioAreaNivel(UsuarioAreaNivelBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pId;
            SqlParameter pIdUsuario;
            SqlParameter pIdArea;
            SqlParameter pIdNivelAprobacion;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioAreaNivelInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.Direction = ParameterDirection.ReturnValue;
                pId.SqlDbType = SqlDbType.Int;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = objBE.IdUsuario;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdArea";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = objBE.IdArea;

                pIdNivelAprobacion = new SqlParameter();
                pIdNivelAprobacion.ParameterName = "@IdNivelAprobacion";
                pIdNivelAprobacion.SqlDbType = SqlDbType.Int;
                pIdNivelAprobacion.Value = objBE.IdNivelAprobacion;

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

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pIdArea);
                sqlCmd.Parameters.Add(pIdNivelAprobacion);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pId.Value);

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

        // Modificar UsuarioAreaNivel
        public void ModificarUsuarioAreaNivel(UsuarioAreaNivelBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            //SqlParameter prmId;
            SqlParameter pIdUsuario;
            SqlParameter pIdArea;
            SqlParameter pIdNivelAprobacion;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioAreaNivelModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //prmId = new SqlParameter();
                //prmId.ParameterName = "@Id";
                //prmId.SqlDbType = SqlDbType.Int;
                //prmId.Value = objBE.Id;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = objBE.IdUsuario;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdArea";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = objBE.IdArea;

                pIdNivelAprobacion = new SqlParameter();
                pIdNivelAprobacion.ParameterName = "@IdNivelAprobacion";
                pIdNivelAprobacion.SqlDbType = SqlDbType.Int;
                pIdNivelAprobacion.Value = objBE.IdNivelAprobacion;

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

                //sqlCmd.Parameters.Add(prmId);
                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pIdArea);
                sqlCmd.Parameters.Add(pIdNivelAprobacion);
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

        // Eliminar UsuarioAreaNivel
        public void EliminarUsuarioAreaNivel(int IdUsuario)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdUsuario;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioAreaNivelEliminar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = IdUsuario;

                sqlCmd.Parameters.Add(pIdUsuario);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();
                
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
