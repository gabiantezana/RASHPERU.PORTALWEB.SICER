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
    public class NivelAprobacionDA
    {
        // Listar NivelAprobacion
        public List<NivelAprobacionBE> ListarNivelAprobacion()
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
                strSP = "MSS_WEB_NivelAprobacionListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<NivelAprobacionBE> lstNivelAprobacionBE;
                NivelAprobacionBE objNivelAprobacionBE;
                lstNivelAprobacionBE = new List<NivelAprobacionBE>();

                while (sqlDR.Read())
                {
                    objNivelAprobacionBE = new NivelAprobacionBE();
                    objNivelAprobacionBE.IdNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdNivel"));
                    objNivelAprobacionBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objNivelAprobacionBE.Nivel = sqlDR.GetString(sqlDR.GetOrdinal("Nivel"));                    
                    objNivelAprobacionBE.Documento = sqlDR.GetString(sqlDR.GetOrdinal("Documento"));
                    objNivelAprobacionBE.EsDeMonto = sqlDR.GetString(sqlDR.GetOrdinal("EsDeMonto"));
                    objNivelAprobacionBE.Monto = sqlDR.GetString(sqlDR.GetOrdinal("Monto"));
                    objNivelAprobacionBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objNivelAprobacionBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objNivelAprobacionBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objNivelAprobacionBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstNivelAprobacionBE.Add(objNivelAprobacionBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstNivelAprobacionBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener NivelAprobacion
        public NivelAprobacionBE ObtenerNivelAprobacion(int Id, int Tipo)
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
                strSP = "MSS_WEB_NivelAprobacionObtener";
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

                NivelAprobacionBE objNivelAprobacionBE;
                objNivelAprobacionBE = null;

                while (sqlDR.Read())
                {
                    objNivelAprobacionBE = new NivelAprobacionBE();
                    objNivelAprobacionBE.IdNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdNivel"));
                    objNivelAprobacionBE.Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("Descripcion"));
                    objNivelAprobacionBE.Nivel = sqlDR.GetString(sqlDR.GetOrdinal("Nivel"));
                    objNivelAprobacionBE.Documento = sqlDR.GetString(sqlDR.GetOrdinal("Documento"));
                    objNivelAprobacionBE.EsDeMonto = sqlDR.GetString(sqlDR.GetOrdinal("EsDeMonto"));
                    objNivelAprobacionBE.Monto = sqlDR.GetString(sqlDR.GetOrdinal("Monto"));
                    objNivelAprobacionBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objNivelAprobacionBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objNivelAprobacionBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objNivelAprobacionBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objNivelAprobacionBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar NivelAprobacion
        public int InsertarNivelAprobacion(NivelAprobacionBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdNivel;
            SqlParameter pDescripcion;
            SqlParameter pNivel;
            SqlParameter pDocumento;
            SqlParameter pEsDeMonto;
            SqlParameter pMonto;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_NivelAprobacionInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdNivel = new SqlParameter();
                pIdNivel.Direction = ParameterDirection.ReturnValue;
                pIdNivel.SqlDbType = SqlDbType.Int;

                pDescripcion = new SqlParameter();
                pDescripcion.ParameterName = "@Descripcion";
                pDescripcion.SqlDbType = SqlDbType.VarChar;
                pDescripcion.Size = 500;
                pDescripcion.Value = objBE.Descripcion;

                pNivel = new SqlParameter();
                pNivel.ParameterName = "@Nivel";
                pNivel.SqlDbType = SqlDbType.VarChar;
                pNivel.Size = 3;
                pNivel.Value = objBE.Nivel;

                pDocumento = new SqlParameter();
                pDocumento.ParameterName = "@Documento";
                pDocumento.SqlDbType = SqlDbType.VarChar;
                pDocumento.Size = 3;
                pDocumento.Value = objBE.Documento;

                pEsDeMonto = new SqlParameter();
                pEsDeMonto.ParameterName = "@EsDeMonto";
                pEsDeMonto.SqlDbType = SqlDbType.VarChar;
                pEsDeMonto.Size = 3;
                pEsDeMonto.Value = objBE.EsDeMonto;

                pMonto = new SqlParameter();
                pMonto.ParameterName = "@Monto";
                pMonto.SqlDbType = SqlDbType.VarChar;
                pMonto.Size = 50;
                pMonto.Value = objBE.Monto;

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

                sqlCmd.Parameters.Add(pIdNivel);
                sqlCmd.Parameters.Add(pDescripcion);
                sqlCmd.Parameters.Add(pNivel);
                sqlCmd.Parameters.Add(pDocumento);
                sqlCmd.Parameters.Add(pEsDeMonto);
                sqlCmd.Parameters.Add(pMonto);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdNivel.Value);

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

        // Modificar NivelAprobacion
        public void ModificarNivelAprobacion(NivelAprobacionBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdNivel;
            SqlParameter pDescripcion;
            SqlParameter pNivel;
            SqlParameter pDocumento;
            SqlParameter pEsDeMonto;
            SqlParameter pMonto;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_NivelAprobacionModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdNivel = new SqlParameter();
                pIdNivel.ParameterName = "@IdNivel";
                pIdNivel.SqlDbType = SqlDbType.Int;
                pIdNivel.Value = objBE.IdNivel;

                pDescripcion = new SqlParameter();
                pDescripcion.ParameterName = "@Descripcion";
                pDescripcion.SqlDbType = SqlDbType.VarChar;
                pDescripcion.Size = 500;
                pDescripcion.Value = objBE.Descripcion;

                pNivel = new SqlParameter();
                pNivel.ParameterName = "@Nivel";
                pNivel.SqlDbType = SqlDbType.VarChar;
                pNivel.Size = 3;
                pNivel.Value = objBE.Nivel;

                pDocumento = new SqlParameter();
                pDocumento.ParameterName = "@Documento";
                pDocumento.SqlDbType = SqlDbType.VarChar;
                pDocumento.Size = 3;
                pDocumento.Value = objBE.Documento;

                pEsDeMonto = new SqlParameter();
                pEsDeMonto.ParameterName = "@EsDeMonto";
                pEsDeMonto.SqlDbType = SqlDbType.VarChar;
                pEsDeMonto.Size = 3;
                pEsDeMonto.Value = objBE.EsDeMonto;

                pMonto = new SqlParameter();
                pMonto.ParameterName = "@Monto";
                pMonto.SqlDbType = SqlDbType.VarChar;
                pMonto.Size = 50;
                pMonto.Value = objBE.Monto;

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

                sqlCmd.Parameters.Add(pIdNivel);
                sqlCmd.Parameters.Add(pDescripcion);
                sqlCmd.Parameters.Add(pNivel);
                sqlCmd.Parameters.Add(pDocumento);
                sqlCmd.Parameters.Add(pEsDeMonto);
                sqlCmd.Parameters.Add(pMonto);
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
