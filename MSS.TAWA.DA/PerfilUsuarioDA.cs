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
    public class PerfilUsuarioDA
    {
        // Listar PerfilUsuario
        public List<PerfilUsuarioBE> ListarPerfilUsuario()
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
                strSP = "MSS_WEB_PerfilUsuarioListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<PerfilUsuarioBE> lstPerfilUsuarioBE;
                PerfilUsuarioBE objPerfilUsuarioBE;
                lstPerfilUsuarioBE = new List<PerfilUsuarioBE>();

                while (sqlDR.Read())
                {
                    objPerfilUsuarioBE = new PerfilUsuarioBE();
                    objPerfilUsuarioBE.IdPerfilUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdPerfilUsuario"));
                    objPerfilUsuarioBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objPerfilUsuarioBE.TipoAprobador = sqlDR.GetString(sqlDR.GetOrdinal("TipoAprobador"));
                    objPerfilUsuarioBE.ModAdministrador = sqlDR.GetString(sqlDR.GetOrdinal("ModAdministrador"));
                    objPerfilUsuarioBE.ModCajaChica = sqlDR.GetString(sqlDR.GetOrdinal("ModCajaChica"));
                    objPerfilUsuarioBE.ModEntregaRendir = sqlDR.GetString(sqlDR.GetOrdinal("ModEntregaRendir"));
                    objPerfilUsuarioBE.ModReembolso = sqlDR.GetString(sqlDR.GetOrdinal("ModReembolso"));
                    objPerfilUsuarioBE.CreaCajaChica = sqlDR.GetString(sqlDR.GetOrdinal("CreaCajaChica"));
                    objPerfilUsuarioBE.CreaEntregaRendir = sqlDR.GetString(sqlDR.GetOrdinal("CreaEntregaRendir"));
                    objPerfilUsuarioBE.CreaReembolso = sqlDR.GetString(sqlDR.GetOrdinal("CreaReembolso"));
                    objPerfilUsuarioBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objPerfilUsuarioBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objPerfilUsuarioBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objPerfilUsuarioBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstPerfilUsuarioBE.Add(objPerfilUsuarioBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstPerfilUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener PerfilUsuario
        public PerfilUsuarioBE ObtenerPerfilUsuario(int Id)
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
                strSP = "MSS_WEB_PerfilUsuarioObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdArea = new SqlParameter();
                pIdArea.ParameterName = "@IdPerfilUsuario";
                pIdArea.SqlDbType = SqlDbType.Int;
                pIdArea.Value = Id;
                sqlCmd.Parameters.Add(pIdArea);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                PerfilUsuarioBE objPerfilUsuarioBE;
                objPerfilUsuarioBE = null;

                while (sqlDR.Read())
                {
                    objPerfilUsuarioBE = new PerfilUsuarioBE();
                    objPerfilUsuarioBE.IdPerfilUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdPerfilUsuario"));
                    objPerfilUsuarioBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objPerfilUsuarioBE.TipoAprobador = sqlDR.GetString(sqlDR.GetOrdinal("TipoAprobador"));
                    objPerfilUsuarioBE.ModAdministrador = sqlDR.GetString(sqlDR.GetOrdinal("ModAdministrador"));
                    objPerfilUsuarioBE.ModCajaChica = sqlDR.GetString(sqlDR.GetOrdinal("ModCajaChica"));
                    objPerfilUsuarioBE.ModEntregaRendir = sqlDR.GetString(sqlDR.GetOrdinal("ModEntregaRendir"));
                    objPerfilUsuarioBE.ModReembolso = sqlDR.GetString(sqlDR.GetOrdinal("ModReembolso"));
                    objPerfilUsuarioBE.CreaCajaChica = sqlDR.GetString(sqlDR.GetOrdinal("CreaCajaChica"));
                    objPerfilUsuarioBE.CreaEntregaRendir = sqlDR.GetString(sqlDR.GetOrdinal("CreaEntregaRendir"));
                    objPerfilUsuarioBE.CreaReembolso = sqlDR.GetString(sqlDR.GetOrdinal("CreaReembolso"));
                    objPerfilUsuarioBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objPerfilUsuarioBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objPerfilUsuarioBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objPerfilUsuarioBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objPerfilUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar PerfilUsuario
        public int InsertarPerfilUsuario(PerfilUsuarioBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdPerfilUsuario;
            SqlParameter pDescripcion;
            SqlParameter pModAdministrador;
            SqlParameter pModCajaChica;
            SqlParameter pModEntregaRendir;
            SqlParameter pModReembolso;
            SqlParameter pTipoAprobador;
            SqlParameter pCreaCajaChica;
            SqlParameter pCreaEntregaRendir;
            SqlParameter pCreaReembolso;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_PerfilUsuarioInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdPerfilUsuario = new SqlParameter();
                pIdPerfilUsuario.Direction = ParameterDirection.ReturnValue;
                pIdPerfilUsuario.SqlDbType = SqlDbType.Int;

                pDescripcion = new SqlParameter();
                pDescripcion.ParameterName = "@Descripcion";
                pDescripcion.SqlDbType = SqlDbType.VarChar;
                pDescripcion.Size = 500;
                pDescripcion.Value = objBE.Descripcion;

                pModAdministrador = new SqlParameter();
                pModAdministrador.ParameterName = "@ModAdministrador";
                pModAdministrador.SqlDbType = SqlDbType.VarChar;
                pModAdministrador.Size = 3;
                pModAdministrador.Value = objBE.ModAdministrador;

                pModCajaChica = new SqlParameter();
                pModCajaChica.ParameterName = "@ModCajaChica";
                pModCajaChica.SqlDbType = SqlDbType.VarChar;
                pModCajaChica.Size = 3;
                pModCajaChica.Value = objBE.ModCajaChica;

                pModEntregaRendir = new SqlParameter();
                pModEntregaRendir.ParameterName = "@ModEntregaRendir";
                pModEntregaRendir.SqlDbType = SqlDbType.VarChar;
                pModEntregaRendir.Size = 3;
                pModEntregaRendir.Value = objBE.ModEntregaRendir;

                pModReembolso = new SqlParameter();
                pModReembolso.ParameterName = "@ModReembolso";
                pModReembolso.SqlDbType = SqlDbType.VarChar;
                pModReembolso.Size = 3;
                pModReembolso.Value = objBE.ModReembolso;

                pTipoAprobador = new SqlParameter();
                pTipoAprobador.ParameterName = "@TipoAprobador";
                pTipoAprobador.SqlDbType = SqlDbType.VarChar;
                pTipoAprobador.Size = 3;
                pTipoAprobador.Value = objBE.TipoAprobador;

                pCreaCajaChica = new SqlParameter();
                pCreaCajaChica.ParameterName = "@CreaCajaChica";
                pCreaCajaChica.SqlDbType = SqlDbType.VarChar;
                pCreaCajaChica.Size = 3;
                pCreaCajaChica.Value = objBE.CreaCajaChica;

                pCreaEntregaRendir = new SqlParameter();
                pCreaEntregaRendir.ParameterName = "@CreaEntregaRendir";
                pCreaEntregaRendir.SqlDbType = SqlDbType.VarChar;
                pCreaEntregaRendir.Size = 3;
                pCreaEntregaRendir.Value = objBE.CreaEntregaRendir;

                pCreaReembolso = new SqlParameter();
                pCreaReembolso.ParameterName = "@CreaReembolso";
                pCreaReembolso.SqlDbType = SqlDbType.VarChar;
                pCreaReembolso.Size = 3;
                pCreaReembolso.Value = objBE.CreaReembolso;

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

                sqlCmd.Parameters.Add(pIdPerfilUsuario);
                sqlCmd.Parameters.Add(pDescripcion);
                sqlCmd.Parameters.Add(pModAdministrador);
                sqlCmd.Parameters.Add(pModCajaChica);
                sqlCmd.Parameters.Add(pModEntregaRendir);
                sqlCmd.Parameters.Add(pModReembolso);
                sqlCmd.Parameters.Add(pTipoAprobador);
                sqlCmd.Parameters.Add(pCreaCajaChica);
                sqlCmd.Parameters.Add(pCreaEntregaRendir);
                sqlCmd.Parameters.Add(pCreaReembolso);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdPerfilUsuario.Value);

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

        // Modificar PerfilUsuario
        public void ModificarPerfilUsuario(PerfilUsuarioBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdPerfilUsuario;
            SqlParameter pDescripcion;
            SqlParameter pModAdministrador;
            SqlParameter pModCajaChica;
            SqlParameter pModEntregaRendir;
            SqlParameter pModReembolso;
            SqlParameter pTipoAprobador;
            SqlParameter pCreaCajaChica;
            SqlParameter pCreaEntregaRendir;
            SqlParameter pCreaReembolso;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_PerfilUsuarioModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdPerfilUsuario = new SqlParameter();
                pIdPerfilUsuario.ParameterName = "@IdPerfilUsuario";
                pIdPerfilUsuario.SqlDbType = SqlDbType.Int;
                pIdPerfilUsuario.Value = objBE.IdPerfilUsuario;

                pDescripcion = new SqlParameter();
                pDescripcion.ParameterName = "@Descripcion";
                pDescripcion.SqlDbType = SqlDbType.VarChar;
                pDescripcion.Size = 500;
                pDescripcion.Value = objBE.Descripcion;

                pModAdministrador = new SqlParameter();
                pModAdministrador.ParameterName = "@ModAdministrador";
                pModAdministrador.SqlDbType = SqlDbType.VarChar;
                pModAdministrador.Size = 3;
                pModAdministrador.Value = objBE.ModAdministrador;

                pModCajaChica = new SqlParameter();
                pModCajaChica.ParameterName = "@ModCajaChica";
                pModCajaChica.SqlDbType = SqlDbType.VarChar;
                pModCajaChica.Size = 3;
                pModCajaChica.Value = objBE.ModCajaChica;

                pModEntregaRendir = new SqlParameter();
                pModEntregaRendir.ParameterName = "@ModEntregaRendir";
                pModEntregaRendir.SqlDbType = SqlDbType.VarChar;
                pModEntregaRendir.Size = 3;
                pModEntregaRendir.Value = objBE.ModEntregaRendir;

                pModReembolso = new SqlParameter();
                pModReembolso.ParameterName = "@ModReembolso";
                pModReembolso.SqlDbType = SqlDbType.VarChar;
                pModReembolso.Size = 3;
                pModReembolso.Value = objBE.ModReembolso;

                pTipoAprobador = new SqlParameter();
                pTipoAprobador.ParameterName = "@TipoAprobador";
                pTipoAprobador.SqlDbType = SqlDbType.VarChar;
                pTipoAprobador.Size = 3;
                pTipoAprobador.Value = objBE.TipoAprobador;

                pCreaCajaChica = new SqlParameter();
                pCreaCajaChica.ParameterName = "@CreaCajaChica";
                pCreaCajaChica.SqlDbType = SqlDbType.VarChar;
                pCreaCajaChica.Size = 3;
                pCreaCajaChica.Value = objBE.CreaCajaChica;

                pCreaEntregaRendir = new SqlParameter();
                pCreaEntregaRendir.ParameterName = "@CreaEntregaRendir";
                pCreaEntregaRendir.SqlDbType = SqlDbType.VarChar;
                pCreaEntregaRendir.Size = 3;
                pCreaEntregaRendir.Value = objBE.CreaEntregaRendir;

                pCreaReembolso = new SqlParameter();
                pCreaReembolso.ParameterName = "@CreaReembolso";
                pCreaReembolso.SqlDbType = SqlDbType.VarChar;
                pCreaReembolso.Size = 3;
                pCreaReembolso.Value = objBE.CreaReembolso;

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
                
                sqlCmd.Parameters.Add(pIdPerfilUsuario);
                sqlCmd.Parameters.Add(pDescripcion);
                sqlCmd.Parameters.Add(pModAdministrador);
                sqlCmd.Parameters.Add(pModCajaChica);
                sqlCmd.Parameters.Add(pModEntregaRendir);
                sqlCmd.Parameters.Add(pModReembolso);
                sqlCmd.Parameters.Add(pTipoAprobador);
                sqlCmd.Parameters.Add(pCreaCajaChica);
                sqlCmd.Parameters.Add(pCreaEntregaRendir);
                sqlCmd.Parameters.Add(pCreaReembolso);
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
