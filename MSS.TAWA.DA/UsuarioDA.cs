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
    public class UsuarioDA
    {
        // Login Usuario
        public UsuarioBE LoginUsuario(String Username, String Password)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pCardCode;
            SqlParameter pPass;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioLogin";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.VarChar;
                pCardCode.Size = 20;
                pCardCode.Value = Username;

                pPass = new SqlParameter();
                pPass.ParameterName = "@Pass";
                pPass.SqlDbType = SqlDbType.VarChar;
                pPass.Size = 200;
                pPass.Value = Password;

                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pPass);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                UsuarioBE objUsuarioBE;

                objUsuarioBE = null;

                while (sqlDR.Read())
                {
                    objUsuarioBE = new UsuarioBE();
                    objUsuarioBE.IdUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuario"));
                    objUsuarioBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objUsuarioBE.Pass = sqlDR.GetString(sqlDR.GetOrdinal("Pass"));
                    objUsuarioBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objUsuarioBE.Tipo = sqlDR.GetString(sqlDR.GetOrdinal("Tipo"));
                    objUsuarioBE.Phone = sqlDR.GetString(sqlDR.GetOrdinal("Phone"));
                    objUsuarioBE.Mail = sqlDR.GetString(sqlDR.GetOrdinal("Mail"));
                    objUsuarioBE.CantMaxCC = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxCC"));
                    objUsuarioBE.CantMaxER = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxER"));
                    objUsuarioBE.CantMaxRE = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxRE"));
                    objUsuarioBE.IdPerfilUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdPerfilUsuario"));
                    objUsuarioBE.IdArea1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea1"));
                    objUsuarioBE.IdArea2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea2"));
                    objUsuarioBE.IdArea3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea3"));
                    objUsuarioBE.IdArea4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea4"));
                    objUsuarioBE.IdArea5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea5"));
                    objUsuarioBE.IdCentroCostos1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos1"));
                    objUsuarioBE.IdCentroCostos2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos2"));
                    objUsuarioBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objUsuarioBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objUsuarioBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objUsuarioBE.IdCentroCostos6 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos6"));
                    objUsuarioBE.IdCentroCostos7 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos7"));
                    objUsuarioBE.IdCentroCostos8 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos8"));
                    objUsuarioBE.IdCentroCostos9 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos9"));
                    objUsuarioBE.IdCentroCostos10 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos10"));
                    objUsuarioBE.IdCentroCostos11 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos11"));
                    objUsuarioBE.IdCentroCostos12 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos12"));
                    objUsuarioBE.IdCentroCostos13 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos13"));
                    objUsuarioBE.IdCentroCostos14 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos14"));
                    objUsuarioBE.IdCentroCostos15 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos15"));
                    objUsuarioBE.IdUsuarioCC1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC1"));
                    objUsuarioBE.IdUsuarioCC2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC2"));
                    objUsuarioBE.IdUsuarioCC3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC3"));
                    objUsuarioBE.IdUsuarioER1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER1"));
                    objUsuarioBE.IdUsuarioER2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER2"));
                    objUsuarioBE.IdUsuarioER3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER3"));
                    objUsuarioBE.IdUsuarioRE1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE1"));
                    objUsuarioBE.IdUsuarioRE2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE2"));
                    objUsuarioBE.IdUsuarioRE3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE3"));
                    objUsuarioBE.Comentario = sqlDR.GetString(sqlDR.GetOrdinal("Comentario"));
                    objUsuarioBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objUsuarioBE.IntentoLogin = sqlDR.GetString(sqlDR.GetOrdinal("IntentoLogin"));
                    objUsuarioBE.HoraMinutoLogin = sqlDR.GetDateTime(sqlDR.GetOrdinal("HoraMinutoLogin"));
                    objUsuarioBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objUsuarioBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objUsuarioBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objUsuarioBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }
                sqlDR.Close();
                sqlDR.Dispose();
                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
                return objUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Listar Usuario
        public List<UsuarioBE> ListarUsuario(int Tipo2, int IdUsuario2, int Tipo3)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pTipo2;
            SqlParameter pIdUsuario2;
            SqlParameter pTipo3;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                pIdUsuario2 = new SqlParameter();
                pIdUsuario2.ParameterName = "@IdUsuario2";
                pIdUsuario2.SqlDbType = SqlDbType.Int;
                pIdUsuario2.Value = IdUsuario2;

                pTipo3 = new SqlParameter();
                pTipo3.ParameterName = "@Tipo3";
                pTipo3.SqlDbType = SqlDbType.Int;
                pTipo3.Value = Tipo3;

                sqlCmd.Parameters.Add(pTipo2);
                sqlCmd.Parameters.Add(pIdUsuario2);
                sqlCmd.Parameters.Add(pTipo3);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<UsuarioBE> lstUsuarioBE;
                UsuarioBE objUsuarioBE;
                lstUsuarioBE = new List<UsuarioBE>();

                while (sqlDR.Read())
                {
                    objUsuarioBE = new UsuarioBE();
                    objUsuarioBE.IdUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuario"));
                    objUsuarioBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objUsuarioBE.Pass = sqlDR.GetString(sqlDR.GetOrdinal("Pass"));
                    objUsuarioBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objUsuarioBE.Tipo = sqlDR.GetString(sqlDR.GetOrdinal("Tipo"));
                    objUsuarioBE.Phone = sqlDR.GetString(sqlDR.GetOrdinal("Phone"));
                    objUsuarioBE.Mail = sqlDR.GetString(sqlDR.GetOrdinal("Mail"));
                    objUsuarioBE.CantMaxCC = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxCC"));
                    objUsuarioBE.CantMaxER = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxER"));
                    objUsuarioBE.CantMaxRE = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxRE"));
                    objUsuarioBE.IdPerfilUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdPerfilUsuario"));
                    objUsuarioBE.IdArea1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea1"));
                    objUsuarioBE.IdArea2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea2"));
                    objUsuarioBE.IdArea3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea3"));
                    objUsuarioBE.IdArea4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea4"));
                    objUsuarioBE.IdArea5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea5"));
                    objUsuarioBE.IdCentroCostos1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos1"));
                    objUsuarioBE.IdCentroCostos2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos2"));
                    objUsuarioBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objUsuarioBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objUsuarioBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objUsuarioBE.IdCentroCostos6 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos6"));
                    objUsuarioBE.IdCentroCostos7 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos7"));
                    objUsuarioBE.IdCentroCostos8 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos8"));
                    objUsuarioBE.IdCentroCostos9 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos9"));
                    objUsuarioBE.IdCentroCostos10 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos10"));
                    objUsuarioBE.IdCentroCostos11 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos11"));
                    objUsuarioBE.IdCentroCostos12 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos12"));
                    objUsuarioBE.IdCentroCostos13 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos13"));
                    objUsuarioBE.IdCentroCostos14 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos14"));
                    objUsuarioBE.IdCentroCostos15 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos15"));
                    objUsuarioBE.IdUsuarioCC1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC1"));
                    objUsuarioBE.IdUsuarioCC2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC2"));
                    objUsuarioBE.IdUsuarioCC3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC3"));
                    objUsuarioBE.IdUsuarioER1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER1"));
                    objUsuarioBE.IdUsuarioER2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER2"));
                    objUsuarioBE.IdUsuarioER3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER3"));
                    objUsuarioBE.IdUsuarioRE1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE1"));
                    objUsuarioBE.IdUsuarioRE2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE2"));
                    objUsuarioBE.IdUsuarioRE3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE3"));
                    objUsuarioBE.Comentario = sqlDR.GetString(sqlDR.GetOrdinal("Comentario"));
                    objUsuarioBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objUsuarioBE.IntentoLogin = sqlDR.GetString(sqlDR.GetOrdinal("IntentoLogin"));
                    objUsuarioBE.HoraMinutoLogin = sqlDR.GetDateTime(sqlDR.GetOrdinal("HoraMinutoLogin"));
                    objUsuarioBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objUsuarioBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objUsuarioBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objUsuarioBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstUsuarioBE.Add(objUsuarioBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Listar Usuario 2
        public List<UsuarioBE> ListarUsuario2(int Tipo2, int Tipo3, int Tipo4, String Palabra)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pTipo2;
            SqlParameter pTipo3;
            SqlParameter pTipo4;
            SqlParameter pPalabra;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioListar2";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                pTipo3 = new SqlParameter();
                pTipo3.ParameterName = "@Tipo3";
                pTipo3.SqlDbType = SqlDbType.Int;
                pTipo3.Value = Tipo3;

                pTipo4 = new SqlParameter();
                pTipo4.ParameterName = "@Tipo4";
                pTipo4.SqlDbType = SqlDbType.Int;
                pTipo4.Value = Tipo4;

                pPalabra = new SqlParameter();
                pPalabra.ParameterName = "@Palabra";
                pPalabra.SqlDbType = SqlDbType.VarChar;
                pPalabra.Size = 100;
                pPalabra.Value = Palabra;

                sqlCmd.Parameters.Add(pTipo2);
                sqlCmd.Parameters.Add(pTipo3);
                sqlCmd.Parameters.Add(pTipo4);
                sqlCmd.Parameters.Add(pPalabra);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<UsuarioBE> lstUsuarioBE;
                UsuarioBE objUsuarioBE;
                lstUsuarioBE = new List<UsuarioBE>();

                while (sqlDR.Read())
                {
                    objUsuarioBE = new UsuarioBE();
                    objUsuarioBE.IdUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuario"));
                    objUsuarioBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objUsuarioBE.Pass = sqlDR.GetString(sqlDR.GetOrdinal("Pass"));
                    objUsuarioBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objUsuarioBE.Tipo = sqlDR.GetString(sqlDR.GetOrdinal("Tipo"));
                    objUsuarioBE.Phone = sqlDR.GetString(sqlDR.GetOrdinal("Phone"));
                    objUsuarioBE.Mail = sqlDR.GetString(sqlDR.GetOrdinal("Mail"));
                    objUsuarioBE.CantMaxCC = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxCC"));
                    objUsuarioBE.CantMaxER = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxER"));
                    objUsuarioBE.CantMaxRE = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxRE"));
                    objUsuarioBE.IdPerfilUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdPerfilUsuario"));
                    objUsuarioBE.IdArea1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea1"));
                    objUsuarioBE.IdArea2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea2"));
                    objUsuarioBE.IdArea3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea3"));
                    objUsuarioBE.IdArea4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea4"));
                    objUsuarioBE.IdArea5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea5"));
                    objUsuarioBE.IdCentroCostos1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos1"));
                    objUsuarioBE.IdCentroCostos2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos2"));
                    objUsuarioBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objUsuarioBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objUsuarioBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objUsuarioBE.IdCentroCostos6 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos6"));
                    objUsuarioBE.IdCentroCostos7 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos7"));
                    objUsuarioBE.IdCentroCostos8 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos8"));
                    objUsuarioBE.IdCentroCostos9 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos9"));
                    objUsuarioBE.IdCentroCostos10 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos10"));
                    objUsuarioBE.IdCentroCostos11 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos11"));
                    objUsuarioBE.IdCentroCostos12 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos12"));
                    objUsuarioBE.IdCentroCostos13 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos13"));
                    objUsuarioBE.IdCentroCostos14 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos14"));
                    objUsuarioBE.IdCentroCostos15 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos15"));
                    objUsuarioBE.IdUsuarioCC1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC1"));
                    objUsuarioBE.IdUsuarioCC2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC2"));
                    objUsuarioBE.IdUsuarioCC3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC3"));
                    objUsuarioBE.IdUsuarioER1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER1"));
                    objUsuarioBE.IdUsuarioER2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER2"));
                    objUsuarioBE.IdUsuarioER3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER3"));
                    objUsuarioBE.IdUsuarioRE1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE1"));
                    objUsuarioBE.IdUsuarioRE2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE2"));
                    objUsuarioBE.IdUsuarioRE3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE3"));
                    objUsuarioBE.Comentario = sqlDR.GetString(sqlDR.GetOrdinal("Comentario"));
                    objUsuarioBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objUsuarioBE.IntentoLogin = sqlDR.GetString(sqlDR.GetOrdinal("IntentoLogin"));
                    objUsuarioBE.HoraMinutoLogin = sqlDR.GetDateTime(sqlDR.GetOrdinal("HoraMinutoLogin"));
                    objUsuarioBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objUsuarioBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objUsuarioBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objUsuarioBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstUsuarioBE.Add(objUsuarioBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModificarContrasena(int CardCode, String Pass)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pCardCode;
            SqlParameter pPass;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioModificarContrasena";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.NVarChar;
                pCardCode.Value = CardCode.ToString();

                pPass = new SqlParameter();
                pPass.ParameterName = "@Pass";
                pPass.SqlDbType = SqlDbType.NVarChar;
                pPass.Value = Pass;

                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pPass);

                try
                {
                    sqlCmd.Connection.Open();
                    sqlCmd.ExecuteNonQuery();

                    sqlCmd.Connection.Close();
                    sqlCmd.Dispose();

                    sqlConn.Close();
                    sqlConn.Dispose();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

 public String VerificarContrasena(String Pass, String Usuario, int CuentaNumerico, int CuentaMayusculas, int CuentaEspeciales)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pPass;
            SqlParameter pUsuario;
            SqlParameter pCuentaNumerico;
            SqlParameter pCuentaMayusculas;
            SqlParameter pCuentaEspeciales;


            int Mayuscula = 0;
            int Numerico = 0;
            int Especial = 0;

            int Repeticion = 0;
            int NumeroRepeticion = 0;
            int DiasVencimiento = 0;
            int NumNumericos = 0;
            int NumMayusculas = 0;
            int NumEspeciales = 0;
            int CarMinContrasena = 0;



            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                
                strSP = "MSS_WEB_NivelSeguridadContraseña";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pPass = new SqlParameter();
                pPass.ParameterName = "@Pass";
                pPass.SqlDbType = SqlDbType.VarChar;
                pPass.Value = Pass;

                pUsuario = new SqlParameter();
                pUsuario.ParameterName = "@Usuario";
                pUsuario.SqlDbType = SqlDbType.VarChar;
                pUsuario.Value = Usuario;

                pCuentaNumerico = new SqlParameter();
                pCuentaNumerico.ParameterName = "@NumNumericos";
                pCuentaNumerico.SqlDbType = SqlDbType.Int;
                pCuentaNumerico.Value = CuentaNumerico;

                pCuentaMayusculas = new SqlParameter();
                pCuentaMayusculas.ParameterName = "@NumMayusculas";
                pCuentaMayusculas.SqlDbType = SqlDbType.Int;
                pCuentaMayusculas.Value = CuentaMayusculas;

                pCuentaEspeciales = new SqlParameter();
                pCuentaEspeciales.ParameterName = "@NumEspeciales";
                pCuentaEspeciales.SqlDbType = SqlDbType.Int;
                pCuentaEspeciales.Value = CuentaEspeciales;

                sqlCmd.Parameters.Add(pPass);
                sqlCmd.Parameters.Add(pUsuario);
                sqlCmd.Parameters.Add(pCuentaNumerico);
                sqlCmd.Parameters.Add(pCuentaMayusculas);
                sqlCmd.Parameters.Add(pCuentaEspeciales);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();



                while (sqlDR.Read())
                {
                    Mayuscula =         Convert.ToInt32(sqlDR.GetInt32(sqlDR.GetOrdinal("Mayusculas")));
                    Numerico =          Convert.ToInt32(sqlDR.GetInt32(sqlDR.GetOrdinal("Numerico")));
                    Especial =          Convert.ToInt32(sqlDR.GetInt32(sqlDR.GetOrdinal("Especial")));
                    Repeticion =        Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("Repeticion")));
                    NumeroRepeticion =  Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("NumeroRepeticion")));
                    DiasVencimiento =   Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("DiasVencimiento")));
                    NumNumericos =      Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("NumNumericos")));
                    NumMayusculas =     Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("NumMayusculas")));
                    NumEspeciales =     Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("NumEspeciales")));
                    CarMinContrasena  = Convert.ToInt32(sqlDR.GetValue(sqlDR.GetOrdinal("CarMinContrasena")));

                }


                sqlDR.Close();
                sqlDR.Dispose();
                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();

                if (DiasVencimiento != 1)
                    return "La Contraseña ha caducado, por favor cambiar la contraseña";

                if (Repeticion != 1)
                    return "La contraseña no puede ser igual a las " + NumeroRepeticion.ToString() + " ultimas contraseñas";

                if (NumNumericos != 0)
                    return "Le contraseña debe tener minimo " + NumNumericos.ToString()  + " caracteres numericos";

                if (NumMayusculas != 0)
                    return "Le contraseña debe tener minimo " + NumMayusculas.ToString() + " caracteres mayuscula";

                if (NumEspeciales != 0)
                    return "Le contraseña debe tener minimo " + NumEspeciales.ToString() + " caracteres especiales";

                if (CarMinContrasena != 0)
                    return "Le contraseña debe tener minimo " + CarMinContrasena.ToString() + " caracteres";

                if (Mayuscula == 1 && Numerico == 1 && Especial == 1)
                    return "Correcto";
                else
                    return "Error";



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }




        }
        // Obtener Usuario
        public int VerificarUsuario(String CardCode)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pCardCode;
            int Resultado = 0;
            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioVerificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.VarChar;
                pCardCode.Value = CardCode;

                sqlCmd.Parameters.Add(pCardCode);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();



                while (sqlDR.Read())
                {
                    Resultado = Convert.ToInt32(sqlDR.GetInt32(sqlDR.GetOrdinal("CardCode")));
                }


                sqlDR.Close();
                sqlDR.Dispose();
                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();

                return Resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public int VerificarUsuario2(int CardCode, String Mail)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pCardCode;
            SqlParameter pMail;
            int Resultado = 0;
            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioVerificar2";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.Int;
                pCardCode.Value = CardCode;

                pMail = new SqlParameter();
                pMail.ParameterName = "@Mail";
                pMail.SqlDbType = SqlDbType.NVarChar;
                pMail.Value = Mail;

                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pMail);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();



                while (sqlDR.Read())
                {
                    Resultado = Convert.ToInt32(sqlDR.GetInt32(sqlDR.GetOrdinal("Resultado")));
                }


                sqlDR.Close();
                sqlDR.Dispose();
                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();

                return Resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public int VerificarUsuarioExiste(int CardCode)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pCardCode;
            int Resultado = 0;
            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioVerificarExiste";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.Int;
                pCardCode.Value = CardCode;


                sqlCmd.Parameters.Add(pCardCode);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();



                while (sqlDR.Read())
                {
                    Resultado = Convert.ToInt32(sqlDR.GetInt32(sqlDR.GetOrdinal("Existe")));
                }


                sqlDR.Close();
                sqlDR.Dispose();
                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();

                return Resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Listar Usuario
        public List<UsuarioBE> ListarUsuarioCorreosTesoreria()
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
                strSP = "MSS_WEB_UsuarioCorreoTesoreria";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<UsuarioBE> lstUsuarioBE;
                UsuarioBE objUsuarioBE;
                lstUsuarioBE = new List<UsuarioBE>();

                while (sqlDR.Read())
                {
                    objUsuarioBE = new UsuarioBE();
                    objUsuarioBE.Mail = sqlDR.GetString(sqlDR.GetOrdinal("Mail"));
                    lstUsuarioBE.Add(objUsuarioBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener Usuario
        public UsuarioBE ObtenerUsuario(int Id, int Tipo)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pIdUsuario;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = Id;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pTipo);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                UsuarioBE objUsuarioBE;
                objUsuarioBE = null;

                while (sqlDR.Read())
                {
                    objUsuarioBE = new UsuarioBE();
                    objUsuarioBE.IdUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuario"));
                    objUsuarioBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objUsuarioBE.Pass = sqlDR.GetString(sqlDR.GetOrdinal("Pass"));
                    objUsuarioBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objUsuarioBE.Tipo = sqlDR.GetString(sqlDR.GetOrdinal("Tipo"));
                    objUsuarioBE.Phone = sqlDR.GetString(sqlDR.GetOrdinal("Phone"));
                    objUsuarioBE.Mail = sqlDR.GetString(sqlDR.GetOrdinal("Mail"));
                    objUsuarioBE.CantMaxCC = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxCC"));
                    objUsuarioBE.CantMaxER = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxER"));
                    objUsuarioBE.CantMaxRE = sqlDR.GetString(sqlDR.GetOrdinal("CantMaxRE"));
                    objUsuarioBE.IdPerfilUsuario = sqlDR.GetInt32(sqlDR.GetOrdinal("IdPerfilUsuario"));
                    objUsuarioBE.IdArea1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea1"));
                    objUsuarioBE.IdArea2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea2"));
                    objUsuarioBE.IdArea3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea3"));
                    objUsuarioBE.IdArea4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea4"));
                    objUsuarioBE.IdArea5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdArea5"));
                    objUsuarioBE.IdCentroCostos1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos1"));
                    objUsuarioBE.IdCentroCostos2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos2"));
                    objUsuarioBE.IdCentroCostos3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos3"));
                    objUsuarioBE.IdCentroCostos4 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos4"));
                    objUsuarioBE.IdCentroCostos5 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos5"));
                    objUsuarioBE.IdCentroCostos6 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos6"));
                    objUsuarioBE.IdCentroCostos7 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos7"));
                    objUsuarioBE.IdCentroCostos8 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos8"));
                    objUsuarioBE.IdCentroCostos9 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos9"));
                    objUsuarioBE.IdCentroCostos10 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos10"));
                    objUsuarioBE.IdCentroCostos11 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos11"));
                    objUsuarioBE.IdCentroCostos12 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos12"));
                    objUsuarioBE.IdCentroCostos13 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos13"));
                    objUsuarioBE.IdCentroCostos14 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos14"));
                    objUsuarioBE.IdCentroCostos15 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdCentroCostos15"));
                    objUsuarioBE.IdUsuarioCC1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC1"));
                    objUsuarioBE.IdUsuarioCC2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC2"));
                    objUsuarioBE.IdUsuarioCC3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioCC3"));
                    objUsuarioBE.IdUsuarioER1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER1"));
                    objUsuarioBE.IdUsuarioER2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER2"));
                    objUsuarioBE.IdUsuarioER3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioER3"));
                    objUsuarioBE.IdUsuarioRE1 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE1"));
                    objUsuarioBE.IdUsuarioRE2 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE2"));
                    objUsuarioBE.IdUsuarioRE3 = sqlDR.GetInt32(sqlDR.GetOrdinal("IdUsuarioRE3"));
                    objUsuarioBE.Comentario = sqlDR.GetString(sqlDR.GetOrdinal("Comentario"));
                    objUsuarioBE.Estado = sqlDR.GetString(sqlDR.GetOrdinal("Estado"));
                    objUsuarioBE.IntentoLogin = sqlDR.GetString(sqlDR.GetOrdinal("IntentoLogin"));
                    objUsuarioBE.HoraMinutoLogin = sqlDR.GetDateTime(sqlDR.GetOrdinal("HoraMinutoLogin"));
                    objUsuarioBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objUsuarioBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objUsuarioBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objUsuarioBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlDR.Close();
                sqlDR.Dispose();
                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
                return objUsuarioBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Insertar Usuario / Grupo Trabajo
        public int InsertarUsuario(UsuarioBE objBE, int Tipo2, int IdUsuario2)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdUsuario;
            SqlParameter pCardCode;
            SqlParameter pPass;
            SqlParameter pCardName;
            SqlParameter pTipo;
            SqlParameter pPhone;
            SqlParameter pMail;
            SqlParameter pCantMaxCC;
            SqlParameter pCantMaxER;
            SqlParameter pCantMaxRE;
            SqlParameter pIdPerfilUsuario;
            SqlParameter pIdArea1;
            SqlParameter pIdArea2;
            SqlParameter pIdArea3;
            SqlParameter pIdArea4;
            SqlParameter pIdArea5;
            SqlParameter pIdCentroCostos1;
            SqlParameter pIdCentroCostos2;
            SqlParameter pIdCentroCostos3;
            SqlParameter pIdCentroCostos4;
            SqlParameter pIdCentroCostos5;
            SqlParameter pIdCentroCostos6;
            SqlParameter pIdCentroCostos7;
            SqlParameter pIdCentroCostos8;
            SqlParameter pIdCentroCostos9;
            SqlParameter pIdCentroCostos10;
            SqlParameter pIdCentroCostos11;
            SqlParameter pIdCentroCostos12;
            SqlParameter pIdCentroCostos13;
            SqlParameter pIdCentroCostos14;
            SqlParameter pIdCentroCostos15;
            SqlParameter pIdUsuarioCC1;
            SqlParameter pIdUsuarioCC2;
            SqlParameter pIdUsuarioCC3;
            SqlParameter pIdUsuarioER1;
            SqlParameter pIdUsuarioER2;
            SqlParameter pIdUsuarioER3;
            SqlParameter pIdUsuarioRE1;
            SqlParameter pIdUsuarioRE2;
            SqlParameter pIdUsuarioRE3;
            SqlParameter pComentario;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            SqlParameter pIdUsuario2;
            SqlParameter pTipo2;

            int Id;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.Direction = ParameterDirection.ReturnValue;
                pIdUsuario.SqlDbType = SqlDbType.Int;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.VarChar;
                pCardCode.Size = 20;
                pCardCode.Value = objBE.CardCode;

                pPass = new SqlParameter();
                pPass.ParameterName = "@Pass";
                pPass.SqlDbType = SqlDbType.VarChar;
                pPass.Size = 200;
                pPass.Value = objBE.Pass;

                pCardName = new SqlParameter();
                pCardName.ParameterName = "@CardName";
                pCardName.SqlDbType = SqlDbType.VarChar;
                pCardName.Size = 100;
                pCardName.Value = objBE.CardName;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.VarChar;
                pTipo.Size = 3;
                pTipo.Value = objBE.Tipo;

                pPhone = new SqlParameter();
                pPhone.ParameterName = "@Phone";
                pPhone.SqlDbType = SqlDbType.VarChar;
                pPhone.Size = 50;
                pPhone.Value = objBE.Phone;

                pMail = new SqlParameter();
                pMail.ParameterName = "@Mail";
                pMail.SqlDbType = SqlDbType.VarChar;
                pMail.Size = 50;
                pMail.Value = objBE.Mail;

                pCantMaxCC = new SqlParameter();
                pCantMaxCC.ParameterName = "@CantMaxCC";
                pCantMaxCC.SqlDbType = SqlDbType.VarChar;
                pCantMaxCC.Size = 10;
                pCantMaxCC.Value = objBE.CantMaxCC;

                pCantMaxER = new SqlParameter();
                pCantMaxER.ParameterName = "@CantMaxER";
                pCantMaxER.SqlDbType = SqlDbType.VarChar;
                pCantMaxER.Size = 10;
                pCantMaxER.Value = objBE.CantMaxER;

                pCantMaxRE = new SqlParameter();
                pCantMaxRE.ParameterName = "@CantMaxRE";
                pCantMaxRE.SqlDbType = SqlDbType.VarChar;
                pCantMaxRE.Size = 10;
                pCantMaxRE.Value = objBE.CantMaxRE;

                pIdPerfilUsuario = new SqlParameter();
                pIdPerfilUsuario.ParameterName = "@IdPerfilUsuario";
                pIdPerfilUsuario.SqlDbType = SqlDbType.Int;
                pIdPerfilUsuario.Value = objBE.IdPerfilUsuario;

                pIdArea1 = new SqlParameter();
                pIdArea1.ParameterName = "@IdArea1";
                pIdArea1.SqlDbType = SqlDbType.Int;
                pIdArea1.Value = objBE.IdArea1;

                pIdArea2 = new SqlParameter();
                pIdArea2.ParameterName = "@IdArea2";
                pIdArea2.SqlDbType = SqlDbType.Int;
                pIdArea2.Value = objBE.IdArea2;

                pIdArea3 = new SqlParameter();
                pIdArea3.ParameterName = "@IdArea3";
                pIdArea3.SqlDbType = SqlDbType.Int;
                pIdArea3.Value = objBE.IdArea3;

                pIdArea4 = new SqlParameter();
                pIdArea4.ParameterName = "@IdArea4";
                pIdArea4.SqlDbType = SqlDbType.Int;
                pIdArea4.Value = objBE.IdArea4;

                pIdArea5 = new SqlParameter();
                pIdArea5.ParameterName = "@IdArea5";
                pIdArea5.SqlDbType = SqlDbType.Int;
                pIdArea5.Value = objBE.IdArea5;

                pIdCentroCostos1 = new SqlParameter();
                pIdCentroCostos1.ParameterName = "@IdCentroCostos1";
                pIdCentroCostos1.SqlDbType = SqlDbType.Int;
                pIdCentroCostos1.Value = objBE.IdCentroCostos1;

                pIdCentroCostos2 = new SqlParameter();
                pIdCentroCostos2.ParameterName = "@IdCentroCostos2";
                pIdCentroCostos2.SqlDbType = SqlDbType.Int;
                pIdCentroCostos2.Value = objBE.IdCentroCostos2;

                pIdCentroCostos3 = new SqlParameter();
                pIdCentroCostos3.ParameterName = "@IdCentroCostos3";
                pIdCentroCostos3.SqlDbType = SqlDbType.Int;
                pIdCentroCostos3.Value = objBE.IdCentroCostos3;

                pIdCentroCostos4 = new SqlParameter();
                pIdCentroCostos4.ParameterName = "@IdCentroCostos4";
                pIdCentroCostos4.SqlDbType = SqlDbType.Int;
                pIdCentroCostos4.Value = objBE.IdCentroCostos4;

                pIdCentroCostos5 = new SqlParameter();
                pIdCentroCostos5.ParameterName = "@IdCentroCostos5";
                pIdCentroCostos5.SqlDbType = SqlDbType.Int;
                pIdCentroCostos5.Value = objBE.IdCentroCostos5;

                pIdCentroCostos6 = new SqlParameter();
                pIdCentroCostos6.ParameterName = "@IdCentroCostos6";
                pIdCentroCostos6.SqlDbType = SqlDbType.Int;
                pIdCentroCostos6.Value = objBE.IdCentroCostos6;

                pIdCentroCostos7 = new SqlParameter();
                pIdCentroCostos7.ParameterName = "@IdCentroCostos7";
                pIdCentroCostos7.SqlDbType = SqlDbType.Int;
                pIdCentroCostos7.Value = objBE.IdCentroCostos7;

                pIdCentroCostos8 = new SqlParameter();
                pIdCentroCostos8.ParameterName = "@IdCentroCostos8";
                pIdCentroCostos8.SqlDbType = SqlDbType.Int;
                pIdCentroCostos8.Value = objBE.IdCentroCostos8;

                pIdCentroCostos9 = new SqlParameter();
                pIdCentroCostos9.ParameterName = "@IdCentroCostos9";
                pIdCentroCostos9.SqlDbType = SqlDbType.Int;
                pIdCentroCostos9.Value = objBE.IdCentroCostos9;

                pIdCentroCostos10 = new SqlParameter();
                pIdCentroCostos10.ParameterName = "@IdCentroCostos10";
                pIdCentroCostos10.SqlDbType = SqlDbType.Int;
                pIdCentroCostos10.Value = objBE.IdCentroCostos10;

                pIdCentroCostos11 = new SqlParameter();
                pIdCentroCostos11.ParameterName = "@IdCentroCostos11";
                pIdCentroCostos11.SqlDbType = SqlDbType.Int;
                pIdCentroCostos11.Value = objBE.IdCentroCostos11;

                pIdCentroCostos12 = new SqlParameter();
                pIdCentroCostos12.ParameterName = "@IdCentroCostos12";
                pIdCentroCostos12.SqlDbType = SqlDbType.Int;
                pIdCentroCostos12.Value = objBE.IdCentroCostos12;

                pIdCentroCostos13 = new SqlParameter();
                pIdCentroCostos13.ParameterName = "@IdCentroCostos13";
                pIdCentroCostos13.SqlDbType = SqlDbType.Int;
                pIdCentroCostos13.Value = objBE.IdCentroCostos13;

                pIdCentroCostos14 = new SqlParameter();
                pIdCentroCostos14.ParameterName = "@IdCentroCostos14";
                pIdCentroCostos14.SqlDbType = SqlDbType.Int;
                pIdCentroCostos14.Value = objBE.IdCentroCostos14;

                pIdCentroCostos15 = new SqlParameter();
                pIdCentroCostos15.ParameterName = "@IdCentroCostos15";
                pIdCentroCostos15.SqlDbType = SqlDbType.Int;
                pIdCentroCostos15.Value = objBE.IdCentroCostos15;

                pIdUsuarioCC1 = new SqlParameter();
                pIdUsuarioCC1.ParameterName = "@IdUsuarioCC1";
                pIdUsuarioCC1.SqlDbType = SqlDbType.Int;
                pIdUsuarioCC1.Value = objBE.IdUsuarioCC1;

                pIdUsuarioCC2 = new SqlParameter();
                pIdUsuarioCC2.ParameterName = "@IdUsuarioCC2";
                pIdUsuarioCC2.SqlDbType = SqlDbType.Int;
                pIdUsuarioCC2.Value = objBE.IdUsuarioCC2;

                pIdUsuarioCC3 = new SqlParameter();
                pIdUsuarioCC3.ParameterName = "@IdUsuarioCC3";
                pIdUsuarioCC3.SqlDbType = SqlDbType.Int;
                pIdUsuarioCC3.Value = objBE.IdUsuarioCC3;

                pIdUsuarioER1 = new SqlParameter();
                pIdUsuarioER1.ParameterName = "@IdUsuarioER1";
                pIdUsuarioER1.SqlDbType = SqlDbType.Int;
                pIdUsuarioER1.Value = objBE.IdUsuarioER1;

                pIdUsuarioER2 = new SqlParameter();
                pIdUsuarioER2.ParameterName = "@IdUsuarioER2";
                pIdUsuarioER2.SqlDbType = SqlDbType.Int;
                pIdUsuarioER2.Value = objBE.IdUsuarioER2;

                pIdUsuarioER3 = new SqlParameter();
                pIdUsuarioER3.ParameterName = "@IdUsuarioER3";
                pIdUsuarioER3.SqlDbType = SqlDbType.Int;
                pIdUsuarioER3.Value = objBE.IdUsuarioER3;

                pIdUsuarioRE1 = new SqlParameter();
                pIdUsuarioRE1.ParameterName = "@IdUsuarioRE1";
                pIdUsuarioRE1.SqlDbType = SqlDbType.Int;
                pIdUsuarioRE1.Value = objBE.IdUsuarioRE1;

                pIdUsuarioRE2 = new SqlParameter();
                pIdUsuarioRE2.ParameterName = "@IdUsuarioRE2";
                pIdUsuarioRE2.SqlDbType = SqlDbType.Int;
                pIdUsuarioRE2.Value = objBE.IdUsuarioRE2;

                pIdUsuarioRE3 = new SqlParameter();
                pIdUsuarioRE3.ParameterName = "@IdUsuarioRE3";
                pIdUsuarioRE3.SqlDbType = SqlDbType.Int;
                pIdUsuarioRE3.Value = objBE.IdUsuarioRE3;

                pComentario = new SqlParameter();
                pComentario.ParameterName = "@Comentario";
                pComentario.SqlDbType = SqlDbType.VarChar;
                pComentario.Size = 1000;
                pComentario.Value = objBE.Comentario;

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

                pTipo2 = new SqlParameter();
                pTipo2.ParameterName = "@Tipo2";
                pTipo2.SqlDbType = SqlDbType.Int;
                pTipo2.Value = Tipo2;

                pIdUsuario2 = new SqlParameter();
                pIdUsuario2.ParameterName = "@IdUsuario2";
                pIdUsuario2.SqlDbType = SqlDbType.Int;
                pIdUsuario2.Value = IdUsuario2;

                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pPass);
                sqlCmd.Parameters.Add(pCardName);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pPhone);
                sqlCmd.Parameters.Add(pMail);
                sqlCmd.Parameters.Add(pCantMaxCC);
                sqlCmd.Parameters.Add(pCantMaxER);
                sqlCmd.Parameters.Add(pCantMaxRE);
                sqlCmd.Parameters.Add(pIdPerfilUsuario);
                sqlCmd.Parameters.Add(pIdArea1);
                sqlCmd.Parameters.Add(pIdArea2);
                sqlCmd.Parameters.Add(pIdArea3);
                sqlCmd.Parameters.Add(pIdArea4);
                sqlCmd.Parameters.Add(pIdArea5);
                sqlCmd.Parameters.Add(pIdCentroCostos1);
                sqlCmd.Parameters.Add(pIdCentroCostos2);
                sqlCmd.Parameters.Add(pIdCentroCostos3);
                sqlCmd.Parameters.Add(pIdCentroCostos4);
                sqlCmd.Parameters.Add(pIdCentroCostos5);
                sqlCmd.Parameters.Add(pIdCentroCostos6);
                sqlCmd.Parameters.Add(pIdCentroCostos7);
                sqlCmd.Parameters.Add(pIdCentroCostos8);
                sqlCmd.Parameters.Add(pIdCentroCostos9);
                sqlCmd.Parameters.Add(pIdCentroCostos10);
                sqlCmd.Parameters.Add(pIdCentroCostos11);
                sqlCmd.Parameters.Add(pIdCentroCostos12);
                sqlCmd.Parameters.Add(pIdCentroCostos13);
                sqlCmd.Parameters.Add(pIdCentroCostos14);
                sqlCmd.Parameters.Add(pIdCentroCostos15);
                sqlCmd.Parameters.Add(pIdUsuarioCC1);
                sqlCmd.Parameters.Add(pIdUsuarioCC2);
                sqlCmd.Parameters.Add(pIdUsuarioCC3);
                sqlCmd.Parameters.Add(pIdUsuarioER1);
                sqlCmd.Parameters.Add(pIdUsuarioER2);
                sqlCmd.Parameters.Add(pIdUsuarioER3);
                sqlCmd.Parameters.Add(pIdUsuarioRE1);
                sqlCmd.Parameters.Add(pIdUsuarioRE2);
                sqlCmd.Parameters.Add(pIdUsuarioRE3);
                sqlCmd.Parameters.Add(pComentario);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);
                sqlCmd.Parameters.Add(pTipo2);
                sqlCmd.Parameters.Add(pIdUsuario2);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdUsuario.Value);

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

public bool InsertarAcceso(int IdLog, DateTime Fecha, String Usuario, String Contraseña, String Operacion)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdLog;
            SqlParameter pFecha;
            SqlParameter pUsuario;
            SqlParameter pContraseña;
            SqlParameter pOperacion;


            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_AccesoInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdLog = new SqlParameter();
                pIdLog.ParameterName = "@IdLog";
                pIdLog.SqlDbType = SqlDbType.Int;
                pIdLog.Size = 20;
                pIdLog.Value = 0;

                pFecha = new SqlParameter();
                pFecha.ParameterName = "@Fecha";
                pFecha.SqlDbType = SqlDbType.DateTime;
                pFecha.Size = 20;
                pFecha.Value = DateTime.Today;

                pUsuario = new SqlParameter();
                pUsuario.ParameterName = "@Usuario";
                pUsuario.SqlDbType = SqlDbType.VarChar;
                pUsuario.Size = 10;
                pUsuario.Value = Usuario;

                pContraseña = new SqlParameter();
                pContraseña.ParameterName = "@Contraseña";
                pContraseña.SqlDbType = SqlDbType.VarChar;
                pContraseña.Size = 100;
                pContraseña.Value = Contraseña;

                pOperacion = new SqlParameter();
                pOperacion.ParameterName = "@Operacion";
                pOperacion.SqlDbType = SqlDbType.VarChar;
                pOperacion.Size = 50;
                pOperacion.Value = Operacion;


                sqlCmd.Parameters.Add(pIdLog);
                sqlCmd.Parameters.Add(pFecha);
                sqlCmd.Parameters.Add(pUsuario);
                sqlCmd.Parameters.Add(pContraseña);
                sqlCmd.Parameters.Add(pOperacion);


                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();


                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();

                return true;
            }

            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        // Modificar Usuario
        public void ModificarUsuario(UsuarioBE objBE)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;

            SqlParameter pIdUsuario;
            SqlParameter pCardCode;
            SqlParameter pPass;
            SqlParameter pCardName;
            SqlParameter pTipo;
            SqlParameter pPhone;
            SqlParameter pMail;
            SqlParameter pCantMaxCC;
            SqlParameter pCantMaxER;
            SqlParameter pCantMaxRE;
            SqlParameter pIdPerfilUsuario;
            SqlParameter pIdArea1;
            SqlParameter pIdArea2;
            SqlParameter pIdArea3;
            SqlParameter pIdArea4;
            SqlParameter pIdArea5;
            SqlParameter pIdCentroCostos1;
            SqlParameter pIdCentroCostos2;
            SqlParameter pIdCentroCostos3;
            SqlParameter pIdCentroCostos4;
            SqlParameter pIdCentroCostos5;
            SqlParameter pIdCentroCostos6;
            SqlParameter pIdCentroCostos7;
            SqlParameter pIdCentroCostos8;
            SqlParameter pIdCentroCostos9;
            SqlParameter pIdCentroCostos10;
            SqlParameter pIdCentroCostos11;
            SqlParameter pIdCentroCostos12;
            SqlParameter pIdCentroCostos13;
            SqlParameter pIdCentroCostos14;
            SqlParameter pIdCentroCostos15;
            SqlParameter pIdUsuarioCC1;
            SqlParameter pIdUsuarioCC2;
            SqlParameter pIdUsuarioCC3;
            SqlParameter pIdUsuarioER1;
            SqlParameter pIdUsuarioER2;
            SqlParameter pIdUsuarioER3;
            SqlParameter pIdUsuarioRE1;
            SqlParameter pIdUsuarioRE2;
            SqlParameter pIdUsuarioRE3;
            SqlParameter pComentario;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_UsuarioModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = objBE.IdUsuario;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.VarChar;
                pCardCode.Size = 20;
                pCardCode.Value = objBE.CardCode;

                pPass = new SqlParameter();
                pPass.ParameterName = "@Pass";
                pPass.SqlDbType = SqlDbType.VarChar;
                pPass.Size = 200;
                pPass.Value = objBE.Pass;

                pCardName = new SqlParameter();
                pCardName.ParameterName = "@CardName";
                pCardName.SqlDbType = SqlDbType.VarChar;
                pCardName.Size = 100;
                pCardName.Value = objBE.CardName;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.VarChar;
                pTipo.Size = 3;
                pTipo.Value = objBE.Tipo;

                pPhone = new SqlParameter();
                pPhone.ParameterName = "@Phone";
                pPhone.SqlDbType = SqlDbType.VarChar;
                pPhone.Size = 50;
                pPhone.Value = objBE.Phone;

                pMail = new SqlParameter();
                pMail.ParameterName = "@Mail";
                pMail.SqlDbType = SqlDbType.VarChar;
                pMail.Size = 50;
                pMail.Value = objBE.Mail;

                pCantMaxCC = new SqlParameter();
                pCantMaxCC.ParameterName = "@CantMaxCC";
                pCantMaxCC.SqlDbType = SqlDbType.VarChar;
                pCantMaxCC.Size = 10;
                pCantMaxCC.Value = objBE.CantMaxCC;

                pCantMaxER = new SqlParameter();
                pCantMaxER.ParameterName = "@CantMaxER";
                pCantMaxER.SqlDbType = SqlDbType.VarChar;
                pCantMaxER.Size = 10;
                pCantMaxER.Value = objBE.CantMaxER;

                pCantMaxRE = new SqlParameter();
                pCantMaxRE.ParameterName = "@CantMaxRE";
                pCantMaxRE.SqlDbType = SqlDbType.VarChar;
                pCantMaxRE.Size = 10;
                pCantMaxRE.Value = objBE.CantMaxRE;

                pIdPerfilUsuario = new SqlParameter();
                pIdPerfilUsuario.ParameterName = "@IdPerfilUsuario";
                pIdPerfilUsuario.SqlDbType = SqlDbType.Int;
                pIdPerfilUsuario.Value = objBE.IdPerfilUsuario;

                pIdArea1 = new SqlParameter();
                pIdArea1.ParameterName = "@IdArea1";
                pIdArea1.SqlDbType = SqlDbType.Int;
                pIdArea1.Value = objBE.IdArea1;

                pIdArea2 = new SqlParameter();
                pIdArea2.ParameterName = "@IdArea2";
                pIdArea2.SqlDbType = SqlDbType.Int;
                pIdArea2.Value = objBE.IdArea2;

                pIdArea3 = new SqlParameter();
                pIdArea3.ParameterName = "@IdArea3";
                pIdArea3.SqlDbType = SqlDbType.Int;
                pIdArea3.Value = objBE.IdArea3;

                pIdArea4 = new SqlParameter();
                pIdArea4.ParameterName = "@IdArea4";
                pIdArea4.SqlDbType = SqlDbType.Int;
                pIdArea4.Value = objBE.IdArea4;

                pIdArea5 = new SqlParameter();
                pIdArea5.ParameterName = "@IdArea5";
                pIdArea5.SqlDbType = SqlDbType.Int;
                pIdArea5.Value = objBE.IdArea5;

                pIdCentroCostos1 = new SqlParameter();
                pIdCentroCostos1.ParameterName = "@IdCentroCostos1";
                pIdCentroCostos1.SqlDbType = SqlDbType.Int;
                pIdCentroCostos1.Value = objBE.IdCentroCostos1;

                pIdCentroCostos2 = new SqlParameter();
                pIdCentroCostos2.ParameterName = "@IdCentroCostos2";
                pIdCentroCostos2.SqlDbType = SqlDbType.Int;
                pIdCentroCostos2.Value = objBE.IdCentroCostos2;

                pIdCentroCostos3 = new SqlParameter();
                pIdCentroCostos3.ParameterName = "@IdCentroCostos3";
                pIdCentroCostos3.SqlDbType = SqlDbType.Int;
                pIdCentroCostos3.Value = objBE.IdCentroCostos3;

                pIdCentroCostos4 = new SqlParameter();
                pIdCentroCostos4.ParameterName = "@IdCentroCostos4";
                pIdCentroCostos4.SqlDbType = SqlDbType.Int;
                pIdCentroCostos4.Value = objBE.IdCentroCostos4;

                pIdCentroCostos5 = new SqlParameter();
                pIdCentroCostos5.ParameterName = "@IdCentroCostos5";
                pIdCentroCostos5.SqlDbType = SqlDbType.Int;
                pIdCentroCostos5.Value = objBE.IdCentroCostos5;

                pIdCentroCostos6 = new SqlParameter();
                pIdCentroCostos6.ParameterName = "@IdCentroCostos6";
                pIdCentroCostos6.SqlDbType = SqlDbType.Int;
                pIdCentroCostos6.Value = objBE.IdCentroCostos6;

                pIdCentroCostos7 = new SqlParameter();
                pIdCentroCostos7.ParameterName = "@IdCentroCostos7";
                pIdCentroCostos7.SqlDbType = SqlDbType.Int;
                pIdCentroCostos7.Value = objBE.IdCentroCostos7;

                pIdCentroCostos8 = new SqlParameter();
                pIdCentroCostos8.ParameterName = "@IdCentroCostos8";
                pIdCentroCostos8.SqlDbType = SqlDbType.Int;
                pIdCentroCostos8.Value = objBE.IdCentroCostos8;

                pIdCentroCostos9 = new SqlParameter();
                pIdCentroCostos9.ParameterName = "@IdCentroCostos9";
                pIdCentroCostos9.SqlDbType = SqlDbType.Int;
                pIdCentroCostos9.Value = objBE.IdCentroCostos9;

                pIdCentroCostos10 = new SqlParameter();
                pIdCentroCostos10.ParameterName = "@IdCentroCostos10";
                pIdCentroCostos10.SqlDbType = SqlDbType.Int;
                pIdCentroCostos10.Value = objBE.IdCentroCostos10;

                pIdCentroCostos11 = new SqlParameter();
                pIdCentroCostos11.ParameterName = "@IdCentroCostos11";
                pIdCentroCostos11.SqlDbType = SqlDbType.Int;
                pIdCentroCostos11.Value = objBE.IdCentroCostos11;

                pIdCentroCostos12 = new SqlParameter();
                pIdCentroCostos12.ParameterName = "@IdCentroCostos12";
                pIdCentroCostos12.SqlDbType = SqlDbType.Int;
                pIdCentroCostos12.Value = objBE.IdCentroCostos12;

                pIdCentroCostos13 = new SqlParameter();
                pIdCentroCostos13.ParameterName = "@IdCentroCostos13";
                pIdCentroCostos13.SqlDbType = SqlDbType.Int;
                pIdCentroCostos13.Value = objBE.IdCentroCostos13;

                pIdCentroCostos14 = new SqlParameter();
                pIdCentroCostos14.ParameterName = "@IdCentroCostos14";
                pIdCentroCostos14.SqlDbType = SqlDbType.Int;
                pIdCentroCostos14.Value = objBE.IdCentroCostos14;

                pIdCentroCostos15 = new SqlParameter();
                pIdCentroCostos15.ParameterName = "@IdCentroCostos15";
                pIdCentroCostos15.SqlDbType = SqlDbType.Int;
                pIdCentroCostos15.Value = objBE.IdCentroCostos15;

                pIdUsuarioCC1 = new SqlParameter();
                pIdUsuarioCC1.ParameterName = "@IdUsuarioCC1";
                pIdUsuarioCC1.SqlDbType = SqlDbType.Int;
                pIdUsuarioCC1.Value = objBE.IdUsuarioCC1;

                pIdUsuarioCC2 = new SqlParameter();
                pIdUsuarioCC2.ParameterName = "@IdUsuarioCC2";
                pIdUsuarioCC2.SqlDbType = SqlDbType.Int;
                pIdUsuarioCC2.Value = objBE.IdUsuarioCC2;

                pIdUsuarioCC3 = new SqlParameter();
                pIdUsuarioCC3.ParameterName = "@IdUsuarioCC3";
                pIdUsuarioCC3.SqlDbType = SqlDbType.Int;
                pIdUsuarioCC3.Value = objBE.IdUsuarioCC3;

                pIdUsuarioER1 = new SqlParameter();
                pIdUsuarioER1.ParameterName = "@IdUsuarioER1";
                pIdUsuarioER1.SqlDbType = SqlDbType.Int;
                pIdUsuarioER1.Value = objBE.IdUsuarioER1;

                pIdUsuarioER2 = new SqlParameter();
                pIdUsuarioER2.ParameterName = "@IdUsuarioER2";
                pIdUsuarioER2.SqlDbType = SqlDbType.Int;
                pIdUsuarioER2.Value = objBE.IdUsuarioER2;

                pIdUsuarioER3 = new SqlParameter();
                pIdUsuarioER3.ParameterName = "@IdUsuarioER3";
                pIdUsuarioER3.SqlDbType = SqlDbType.Int;
                pIdUsuarioER3.Value = objBE.IdUsuarioER3;

                pIdUsuarioRE1 = new SqlParameter();
                pIdUsuarioRE1.ParameterName = "@IdUsuarioRE1";
                pIdUsuarioRE1.SqlDbType = SqlDbType.Int;
                pIdUsuarioRE1.Value = objBE.IdUsuarioRE1;

                pIdUsuarioRE2 = new SqlParameter();
                pIdUsuarioRE2.ParameterName = "@IdUsuarioRE2";
                pIdUsuarioRE2.SqlDbType = SqlDbType.Int;
                pIdUsuarioRE2.Value = objBE.IdUsuarioRE2;

                pIdUsuarioRE3 = new SqlParameter();
                pIdUsuarioRE3.ParameterName = "@IdUsuarioRE3";
                pIdUsuarioRE3.SqlDbType = SqlDbType.Int;
                pIdUsuarioRE3.Value = objBE.IdUsuarioRE3;

                pComentario = new SqlParameter();
                pComentario.ParameterName = "@Comentario";
                pComentario.SqlDbType = SqlDbType.VarChar;
                pComentario.Size = 1000;
                pComentario.Value = objBE.Comentario;

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

                sqlCmd.Parameters.Add(pIdUsuario);
                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pPass);
                sqlCmd.Parameters.Add(pCardName);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pPhone);
                sqlCmd.Parameters.Add(pMail);
                sqlCmd.Parameters.Add(pCantMaxCC);
                sqlCmd.Parameters.Add(pCantMaxER);
                sqlCmd.Parameters.Add(pCantMaxRE);
                sqlCmd.Parameters.Add(pIdPerfilUsuario);
                sqlCmd.Parameters.Add(pIdArea1);
                sqlCmd.Parameters.Add(pIdArea2);
                sqlCmd.Parameters.Add(pIdArea3);
                sqlCmd.Parameters.Add(pIdArea4);
                sqlCmd.Parameters.Add(pIdArea5);
                sqlCmd.Parameters.Add(pIdCentroCostos1);
                sqlCmd.Parameters.Add(pIdCentroCostos2);
                sqlCmd.Parameters.Add(pIdCentroCostos3);
                sqlCmd.Parameters.Add(pIdCentroCostos4);
                sqlCmd.Parameters.Add(pIdCentroCostos5);
                sqlCmd.Parameters.Add(pIdCentroCostos6);
                sqlCmd.Parameters.Add(pIdCentroCostos7);
                sqlCmd.Parameters.Add(pIdCentroCostos8);
                sqlCmd.Parameters.Add(pIdCentroCostos9);
                sqlCmd.Parameters.Add(pIdCentroCostos10);
                sqlCmd.Parameters.Add(pIdCentroCostos11);
                sqlCmd.Parameters.Add(pIdCentroCostos12);
                sqlCmd.Parameters.Add(pIdCentroCostos13);
                sqlCmd.Parameters.Add(pIdCentroCostos14);
                sqlCmd.Parameters.Add(pIdCentroCostos15);
                sqlCmd.Parameters.Add(pIdUsuarioCC1);
                sqlCmd.Parameters.Add(pIdUsuarioCC2);
                sqlCmd.Parameters.Add(pIdUsuarioCC3);
                sqlCmd.Parameters.Add(pIdUsuarioER1);
                sqlCmd.Parameters.Add(pIdUsuarioER2);
                sqlCmd.Parameters.Add(pIdUsuarioER3);
                sqlCmd.Parameters.Add(pIdUsuarioRE1);
                sqlCmd.Parameters.Add(pIdUsuarioRE2);
                sqlCmd.Parameters.Add(pIdUsuarioRE3);
                sqlCmd.Parameters.Add(pComentario);
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

        // Eliminar Usuario / Grupo Trabajo
        public void EliminarUsuario(int Tipo, int IdUsuario)
        {
            SqlConnection sqlConn;
            String strConn;
            SqlCommand sqlCmd;
            String strSP;
            SqlDataReader sqlDR;

            SqlParameter pTipo;
            SqlParameter pIdUsuario;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_UsuarioEliminar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pTipo = new SqlParameter();
                pTipo.ParameterName = "@Tipo";
                pTipo.SqlDbType = SqlDbType.Int;
                pTipo.Value = Tipo;

                pIdUsuario = new SqlParameter();
                pIdUsuario.ParameterName = "@IdUsuario";
                pIdUsuario.SqlDbType = SqlDbType.Int;
                pIdUsuario.Value = IdUsuario;

                sqlCmd.Parameters.Add(pTipo);
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
