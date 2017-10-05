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
    public class GrupoTrabajoDA
    {
        // Listar GrupoTrabajo
        public List<GrupoTrabajoBE> ListarGrupoTrabajo(int Id, int Tipo)
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
                strSP = "MSS_WEB_GrupoTrabajoListar";
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

                List<GrupoTrabajoBE> lstGrupoTrabajoBE;
                GrupoTrabajoBE objGrupoTrabajoBE;
                lstGrupoTrabajoBE = new List<GrupoTrabajoBE>();

                while (sqlDR.Read())
                {
                    objGrupoTrabajoBE = new GrupoTrabajoBE();
                    objGrupoTrabajoBE.Id = sqlDR.GetInt32(sqlDR.GetOrdinal("Id"));
                    objGrupoTrabajoBE.IdUsuarioNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioNivel"));
                    objGrupoTrabajoBE.IdUsuarioSubNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioSubNivel"));
                    objGrupoTrabajoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objGrupoTrabajoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objGrupoTrabajoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objGrupoTrabajoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstGrupoTrabajoBE.Add(objGrupoTrabajoBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstGrupoTrabajoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener GrupoTrabajo
        public GrupoTrabajoBE ObtenerGrupoTrabajo(int Id)
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
                strSP = "MSS_WEB_GrupoTrabajoObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = Id;
                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                GrupoTrabajoBE objGrupoTrabajoBE;
                objGrupoTrabajoBE = null;

                while (sqlDR.Read())
                {
                    objGrupoTrabajoBE = new GrupoTrabajoBE();
                    objGrupoTrabajoBE.Id = sqlDR.GetInt32(sqlDR.GetOrdinal("Id"));
                    objGrupoTrabajoBE.IdUsuarioNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioNivel"));
                    objGrupoTrabajoBE.IdUsuarioSubNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioSubNivel"));
                    objGrupoTrabajoBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objGrupoTrabajoBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objGrupoTrabajoBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objGrupoTrabajoBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objGrupoTrabajoBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar GrupoTrabajo
        public int InsertarGrupoTrabajo(GrupoTrabajoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pId;
            SqlParameter pIdUsuarioNivel;
            SqlParameter pIdUsuarioSubNivel;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_GrupoTrabajoInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.Direction = ParameterDirection.ReturnValue;
                pId.SqlDbType = SqlDbType.Int;

                pIdUsuarioNivel = new SqlParameter();
                pIdUsuarioNivel.ParameterName = "@IdUsuarioNivel";
                pIdUsuarioNivel.SqlDbType = SqlDbType.Int;
                pIdUsuarioNivel.Value = objBE.IdUsuarioNivel;

                pIdUsuarioSubNivel = new SqlParameter();
                pIdUsuarioSubNivel.ParameterName = "@IdUsuarioSubNivel";
                pIdUsuarioSubNivel.SqlDbType = SqlDbType.Int;
                pIdUsuarioSubNivel.Value = objBE.IdUsuarioSubNivel;

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
                sqlCmd.Parameters.Add(pIdUsuarioNivel);
                sqlCmd.Parameters.Add(pIdUsuarioSubNivel);
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

        // Modificar GrupoTrabajo
        public void ModificarGrupoTrabajo(GrupoTrabajoBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pId;
            SqlParameter pIdUsuarioNivel;
            SqlParameter pIdUsuarioSubNivel;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_GrupoTrabajoModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@Id";
                pId.SqlDbType = SqlDbType.Int;
                pId.Value = objBE.Id;

                pIdUsuarioNivel = new SqlParameter();
                pIdUsuarioNivel.ParameterName = "@IdUsuarioNivel";
                pIdUsuarioNivel.SqlDbType = SqlDbType.Int;
                pIdUsuarioNivel.Value = objBE.IdUsuarioNivel;

                pIdUsuarioSubNivel = new SqlParameter();
                pIdUsuarioSubNivel.ParameterName = "@IdUsuarioSubNivel";
                pIdUsuarioSubNivel.SqlDbType = SqlDbType.Int;
                pIdUsuarioSubNivel.Value = objBE.IdUsuarioSubNivel;

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
                sqlCmd.Parameters.Add(pIdUsuarioNivel);
                sqlCmd.Parameters.Add(pIdUsuarioSubNivel);
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
