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
    public class NivelSeguridadDA
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

        // Obtener NivelSeguridad
        public NivelSeguridadBE ObtenerNivelSeguridad()
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
                strSP = "MSS_WEB_NivelSeguridadListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                NivelSeguridadBE objNivelSeguridadBE;
                objNivelSeguridadBE = null;
                while (sqlDR.Read())
                {

                    objNivelSeguridadBE = new NivelSeguridadBE();
                    objNivelSeguridadBE.IdNivel = sqlDR.GetInt32(sqlDR.GetOrdinal("IdNivelSeguridad"));
                    objNivelSeguridadBE.NivelSeguridad = sqlDR.GetString(sqlDR.GetOrdinal("NivelSeguridad"));
                    objNivelSeguridadBE.Activo = sqlDR.GetBoolean(sqlDR.GetOrdinal("Activo"));
                    objNivelSeguridadBE.CaracterNumerico = sqlDR.GetBoolean(sqlDR.GetOrdinal("CaracterNumerico"));
                    objNivelSeguridadBE.CaracterMayuscula = sqlDR.GetBoolean(sqlDR.GetOrdinal("CaracterMayuscula"));
                    objNivelSeguridadBE.CaracterEspecial = sqlDR.GetBoolean(sqlDR.GetOrdinal("CaracterEspecial"));
                    objNivelSeguridadBE.DiasVencimiento = sqlDR.GetInt32(sqlDR.GetOrdinal("DiasVencimiento"));
                    objNivelSeguridadBE.NoRepetirContrasena = sqlDR.GetInt32(sqlDR.GetOrdinal("NoRepetirContrasena"));
                    objNivelSeguridadBE.NumNumericos = sqlDR.GetInt32(sqlDR.GetOrdinal("NumNumericos"));
                    objNivelSeguridadBE.NumMayusculas = sqlDR.GetInt32(sqlDR.GetOrdinal("NumMayusculas"));
                    objNivelSeguridadBE.NumEspeciales = sqlDR.GetInt32(sqlDR.GetOrdinal("NumEspeciales"));
                    objNivelSeguridadBE.NumCarContrasena = sqlDR.GetInt32(sqlDR.GetOrdinal("CarMinContrasena"));

                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objNivelSeguridadBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insertar NivelAprobacion
        //public int InsertarNivelAprobacion(NivelSeguridadBE  objBE)
        //{
        //    SqlConnection sqlConn;
        //    String strConn;
        //    SqlCommand sqlCmd;
        //    String strSP;

        //    SqlParameter pIdNivel;
        //    SqlParameter pDescripcion;
        //    SqlParameter pNivel;
        //    SqlParameter pDocumento;
        //    SqlParameter pEsDeMonto;
        //    SqlParameter pMonto;
        //    SqlParameter pUserCreate;
        //    SqlParameter pCreateDate;
        //    SqlParameter pUserUpdate;
        //    SqlParameter pUpdateDate;

        //    int Id;

        //    try
        //    {
        //        strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
        //        sqlConn = new SqlConnection(strConn);
        //        strSP = "MSS_WEB_NivelAprobacionInsertar";
        //        sqlCmd = new SqlCommand(strSP, sqlConn);
        //        sqlCmd.CommandType = CommandType.StoredProcedure;

        //        pIdNivel = new SqlParameter();
        //        pIdNivel.Direction = ParameterDirection.ReturnValue;
        //        pIdNivel.SqlDbType = SqlDbType.Int;

        //        pDescripcion = new SqlParameter();
        //        pDescripcion.ParameterName = "@Descripcion";
        //        pDescripcion.SqlDbType = SqlDbType.VarChar;
        //        pDescripcion.Size = 500;
        //        pDescripcion.Value = objBE.Descripcion;

        //        pNivel = new SqlParameter();
        //        pNivel.ParameterName = "@Nivel";
        //        pNivel.SqlDbType = SqlDbType.VarChar;
        //        pNivel.Size = 3;
        //        pNivel.Value = objBE.Nivel;

        //        pDocumento = new SqlParameter();
        //        pDocumento.ParameterName = "@Documento";
        //        pDocumento.SqlDbType = SqlDbType.VarChar;
        //        pDocumento.Size = 3;
        //        pDocumento.Value = objBE.Documento;

        //        pEsDeMonto = new SqlParameter();
        //        pEsDeMonto.ParameterName = "@EsDeMonto";
        //        pEsDeMonto.SqlDbType = SqlDbType.VarChar;
        //        pEsDeMonto.Size = 3;
        //        pEsDeMonto.Value = objBE.EsDeMonto;

        //        pMonto = new SqlParameter();
        //        pMonto.ParameterName = "@Monto";
        //        pMonto.SqlDbType = SqlDbType.VarChar;
        //        pMonto.Size = 50;
        //        pMonto.Value = objBE.Monto;

        //        pUserCreate = new SqlParameter();
        //        pUserCreate.ParameterName = "@UserCreate";
        //        pUserCreate.SqlDbType = SqlDbType.VarChar;
        //        pUserCreate.Size = 20;
        //        pUserCreate.Value = objBE.UserCreate;

        //        pCreateDate = new SqlParameter();
        //        pCreateDate.ParameterName = "@CreateDate";
        //        pCreateDate.SqlDbType = SqlDbType.DateTime;
        //        pCreateDate.Value = objBE.CreateDate;

        //        pUserUpdate = new SqlParameter();
        //        pUserUpdate.ParameterName = "@UserUpdate";
        //        pUserUpdate.SqlDbType = SqlDbType.VarChar;
        //        pUserUpdate.Size = 20;
        //        pUserUpdate.Value = objBE.UserUpdate;

        //        pUpdateDate = new SqlParameter();
        //        pUpdateDate.ParameterName = "@UpdateDate";
        //        pUpdateDate.SqlDbType = SqlDbType.DateTime;
        //        pUpdateDate.Value = objBE.UpdateDate;

        //        sqlCmd.Parameters.Add(pIdNivel);
        //        sqlCmd.Parameters.Add(pDescripcion);
        //        sqlCmd.Parameters.Add(pNivel);
        //        sqlCmd.Parameters.Add(pDocumento);
        //        sqlCmd.Parameters.Add(pEsDeMonto);
        //        sqlCmd.Parameters.Add(pMonto);
        //        sqlCmd.Parameters.Add(pUserCreate);
        //        sqlCmd.Parameters.Add(pCreateDate);
        //        sqlCmd.Parameters.Add(pUserUpdate);
        //        sqlCmd.Parameters.Add(pUpdateDate);

        //        sqlCmd.Connection.Open();
        //        sqlCmd.ExecuteNonQuery();
        //        Id = Convert.ToInt32(pIdNivel.Value);

        //        sqlCmd.Connection.Close();
        //        sqlCmd.Dispose();
        //        sqlConn.Close();
        //        sqlConn.Dispose();

        //        return Id;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        // Modificar NivelSeguridad

        public void ModificarNivelSeguridad(NivelSeguridadBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdNivelSeguridad;
            SqlParameter pNivelSeguridad;
            SqlParameter pActivo;
            SqlParameter pCaracterNumerico;
            SqlParameter pCaracterMayuscula;
            SqlParameter pCaracterEspecial;
            SqlParameter pDiasVencimiento;
            SqlParameter pNoRepetirContrasen;
            SqlParameter pNumNumericos;
            SqlParameter pNumMayusculas;
            SqlParameter pNumEspeciales;
            SqlParameter pNumCarContrasena;


            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_NivelSeguridadModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdNivelSeguridad = new SqlParameter();
                pIdNivelSeguridad.ParameterName = "@IdNivelSeguridad";
                pIdNivelSeguridad.SqlDbType = SqlDbType.Int;
                pIdNivelSeguridad.Value = objBE.IdNivel;

                pNivelSeguridad = new SqlParameter();
                pNivelSeguridad.ParameterName = "@NivelSeguridad";
                pNivelSeguridad.SqlDbType = SqlDbType.NVarChar;
                pNivelSeguridad.Size = 500;
                pNivelSeguridad.Value = objBE.NivelSeguridad;

                pActivo = new SqlParameter();
                pActivo.ParameterName = "@Activo";
                pActivo.SqlDbType = SqlDbType.Int;
                pActivo.Size = 1;
                pActivo.Value = objBE.Activo;

                pCaracterNumerico = new SqlParameter();
                pCaracterNumerico.ParameterName = "@CaracterNumerico";
                pCaracterNumerico.SqlDbType = SqlDbType.Int;
                pCaracterNumerico.Size = 1;
                pCaracterNumerico.Value = objBE.CaracterNumerico;

                pCaracterMayuscula = new SqlParameter();
                pCaracterMayuscula.ParameterName = "@CaracterMayuscula";
                pCaracterMayuscula.SqlDbType = SqlDbType.Int;
                pCaracterMayuscula.Size = 1;
                pCaracterMayuscula.Value = objBE.CaracterMayuscula;

                pCaracterEspecial = new SqlParameter();
                pCaracterEspecial.ParameterName = "@CaracterEspecial";
                pCaracterEspecial.SqlDbType = SqlDbType.Int;
                pCaracterEspecial.Size = 1;
                pCaracterEspecial.Value = objBE.CaracterEspecial;

                pDiasVencimiento = new SqlParameter();
                pDiasVencimiento.ParameterName = "@DiasVencimiento";
                pDiasVencimiento.SqlDbType = SqlDbType.Int;
                pDiasVencimiento.Size = 50;
                pDiasVencimiento.Value = objBE.DiasVencimiento;

                pNoRepetirContrasen = new SqlParameter();
                pNoRepetirContrasen.ParameterName = "@NoRepetirContrasena";
                pNoRepetirContrasen.SqlDbType = SqlDbType.Int;
                pNoRepetirContrasen.Size = 20;
                pNoRepetirContrasen.Value = objBE.NoRepetirContrasena;

                pNumNumericos = new SqlParameter();
                pNumNumericos.ParameterName = "@NumNumericos";
                pNumNumericos.SqlDbType = SqlDbType.Int;
                pNumNumericos.Size = 50;
                pNumNumericos.Value = objBE.NumNumericos;

                pNumMayusculas = new SqlParameter();
                pNumMayusculas.ParameterName = "@NumMayusculas";
                pNumMayusculas.SqlDbType = SqlDbType.Int;
                pNumMayusculas.Size = 50;
                pNumMayusculas.Value = objBE.NumMayusculas;

                pNumEspeciales = new SqlParameter();
                pNumEspeciales.ParameterName = "@NumEspeciales";
                pNumEspeciales.SqlDbType = SqlDbType.Int;
                pNumEspeciales.Size = 50;
                pNumEspeciales.Value = objBE.NumEspeciales;

                pNumCarContrasena = new SqlParameter();
                pNumCarContrasena.ParameterName = "@NumCarContrasena";
                pNumCarContrasena.SqlDbType = SqlDbType.Int;
                pNumCarContrasena.Size = 50;
                pNumCarContrasena.Value = objBE.NumCarContrasena;



                sqlCmd.Parameters.Add(pIdNivelSeguridad);
                sqlCmd.Parameters.Add(pNivelSeguridad);
                sqlCmd.Parameters.Add(pActivo);
                sqlCmd.Parameters.Add(pCaracterNumerico);
                sqlCmd.Parameters.Add(pCaracterMayuscula);
                sqlCmd.Parameters.Add(pCaracterEspecial);
                sqlCmd.Parameters.Add(pDiasVencimiento);
                sqlCmd.Parameters.Add(pNoRepetirContrasen);
                sqlCmd.Parameters.Add(pNumNumericos);
                sqlCmd.Parameters.Add(pNumMayusculas);
                sqlCmd.Parameters.Add(pNumEspeciales);
                sqlCmd.Parameters.Add(pNumCarContrasena);


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
