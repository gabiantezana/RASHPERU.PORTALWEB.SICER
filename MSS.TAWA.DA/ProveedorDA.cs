using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class ProveedorDA
    {
        // Listar Proveedor
        public List<ProveedorBE> ListarProveedor(int Id, int Tipo)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_ProveedorListar";
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

                List<ProveedorBE> lstProveedorBE;
                ProveedorBE objProveedorBE;
                lstProveedorBE = new List<ProveedorBE>();

                while (sqlDR.Read())
                {
                    objProveedorBE = new ProveedorBE();
                    objProveedorBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objProveedorBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objProveedorBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objProveedorBE.TipoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("TipoDocumento"));
                    objProveedorBE.Documento = sqlDR.GetString(sqlDR.GetOrdinal("Documento"));
                    objProveedorBE.Proceso = sqlDR.GetInt32(sqlDR.GetOrdinal("Proceso"));
                    objProveedorBE.IdProceso = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProceso"));
                    objProveedorBE.Estado = sqlDR.GetInt32(sqlDR.GetOrdinal("Estado"));
                    objProveedorBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objProveedorBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objProveedorBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objProveedorBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                    lstProveedorBE.Add(objProveedorBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstProveedorBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProveedorBE> ListarProveedoresDeSAP()
        {
            var url = "businesspartners/getbusinesspartnerlist.xsjs";
            var response = UtilDA.GetJsonResponse(url, typeof(List<ProveedorBE>));
            return response;
        }
        // Obtener Proveedor
        public ProveedorBE ObtenerProveedor(int Id, int Tipo, string Nombre)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;
            SqlParameter pNombre;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_ProveedorObtener";
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

                pNombre = new SqlParameter();
                pNombre.ParameterName = "@Nombre";
                pNombre.SqlDbType = SqlDbType.VarChar;
                pNombre.Size = 100;
                pNombre.Value = Nombre;

                sqlCmd.Parameters.Add(pId);
                sqlCmd.Parameters.Add(pTipo);
                sqlCmd.Parameters.Add(pNombre);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                ProveedorBE objProveedorBE;
                objProveedorBE = null;

                while (sqlDR.Read())
                {
                    objProveedorBE = new ProveedorBE();
                    objProveedorBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objProveedorBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objProveedorBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objProveedorBE.TipoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("TipoDocumento"));
                    objProveedorBE.Documento = sqlDR.GetString(sqlDR.GetOrdinal("Documento"));
                    objProveedorBE.Proceso = sqlDR.GetInt32(sqlDR.GetOrdinal("Proceso"));
                    objProveedorBE.IdProceso = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProceso"));
                    objProveedorBE.Estado = sqlDR.GetInt32(sqlDR.GetOrdinal("Estado"));
                    objProveedorBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objProveedorBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objProveedorBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objProveedorBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objProveedorBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Obtener Proveedor
        public ProveedorBE ObtenerProveedorPorDocumento(string documento)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;
            SqlDataReader sqlDR;

            SqlParameter pId;
            SqlParameter pTipo;
            SqlParameter pNombre;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);
                strSP = "MSS_WEB_ProveedorObtenerPorDocumento";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pNombre = new SqlParameter();
                pNombre.ParameterName = "@Documento";
                pNombre.SqlDbType = SqlDbType.VarChar;
                pNombre.Size = 100;
                pNombre.Value = documento;

                sqlCmd.Parameters.Add(pNombre);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                ProveedorBE objProveedorBE;
                objProveedorBE = null;

                while (sqlDR.Read())
                {
                    objProveedorBE = new ProveedorBE();
                    objProveedorBE.IdProveedor = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProveedor"));
                    objProveedorBE.CardCode = sqlDR.GetString(sqlDR.GetOrdinal("CardCode"));
                    objProveedorBE.CardName = sqlDR.GetString(sqlDR.GetOrdinal("CardName"));
                    objProveedorBE.TipoDocumento = sqlDR.GetString(sqlDR.GetOrdinal("TipoDocumento"));
                    objProveedorBE.Documento = sqlDR.GetString(sqlDR.GetOrdinal("Documento"));
                    objProveedorBE.Proceso = sqlDR.GetInt32(sqlDR.GetOrdinal("Proceso"));
                    objProveedorBE.IdProceso = sqlDR.GetInt32(sqlDR.GetOrdinal("IdProceso"));
                    objProveedorBE.Estado = sqlDR.GetInt32(sqlDR.GetOrdinal("Estado"));
                    objProveedorBE.UserCreate = sqlDR.GetString(sqlDR.GetOrdinal("UserCreate"));
                    objProveedorBE.CreateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("CreateDate"));
                    objProveedorBE.UserUpdate = sqlDR.GetString(sqlDR.GetOrdinal("UserUpdate"));
                    objProveedorBE.UpdateDate = sqlDR.GetDateTime(sqlDR.GetOrdinal("UpdateDate"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return objProveedorBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //OBTENER PROVEEDOR DE SAP
        public string ObtenerProveedorDeSAP(string cardCode)
        {
            var url = "businesspartners/getbusinesspartner.xsjs?cardCode=" + cardCode;
            var response = UtilDA.GetJsonResponse(url, null, "CardName");
            return response;
        }

        public string GetCardCodeProveedorSAP(string ruc)
        {
            var url = "businesspartners/getbusinesspartner.xsjs?cardCode=" + ruc;
            var response = UtilDA.GetJsonResponse(url, null, "CardCode");
            return response;
        }

        // Insertar Proveedor
        public int InsertarProveedor(ProveedorBE objBE)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;

            SqlParameter pIdProveedor;
            SqlParameter pCardCode;
            SqlParameter pCardName;
            SqlParameter pTipoDocumento;
            SqlParameter pDocumento;
            SqlParameter pProceso;
            SqlParameter pIdProceso;
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
                strSP = "MSS_WEB_ProveedorInsertar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdProveedor = new SqlParameter();
                pIdProveedor.Direction = ParameterDirection.ReturnValue;
                pIdProveedor.SqlDbType = SqlDbType.Int;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.VarChar;
                pCardCode.Size = 20;
                pCardCode.Value = objBE.CardCode;

                pCardName = new SqlParameter();
                pCardName.ParameterName = "@CardName";
                pCardName.SqlDbType = SqlDbType.VarChar;
                pCardName.Size = 100;
                pCardName.Value = objBE.CardName;

                pTipoDocumento = new SqlParameter();
                pTipoDocumento.ParameterName = "@TipoDocumento";
                pTipoDocumento.SqlDbType = SqlDbType.VarChar;
                pTipoDocumento.Size = 3;
                pTipoDocumento.Value = objBE.TipoDocumento;

                pDocumento = new SqlParameter();
                pDocumento.ParameterName = "@Documento";
                pDocumento.SqlDbType = SqlDbType.VarChar;
                pDocumento.Size = 11;
                pDocumento.Value = objBE.Documento;

                pProceso = new SqlParameter();
                pProceso.ParameterName = "@Proceso";
                pProceso.SqlDbType = SqlDbType.Int;
                pProceso.Value = objBE.Proceso;

                pIdProceso = new SqlParameter();
                pIdProceso.ParameterName = "@IdProceso";
                pIdProceso.SqlDbType = SqlDbType.Int;
                pIdProceso.Value = objBE.IdProceso;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.Int;
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

                sqlCmd.Parameters.Add(pIdProveedor);
                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pCardName);
                sqlCmd.Parameters.Add(pTipoDocumento);
                sqlCmd.Parameters.Add(pDocumento);
                sqlCmd.Parameters.Add(pProceso);
                sqlCmd.Parameters.Add(pIdProceso);
                sqlCmd.Parameters.Add(pEstado);
                sqlCmd.Parameters.Add(pUserCreate);
                sqlCmd.Parameters.Add(pCreateDate);
                sqlCmd.Parameters.Add(pUserUpdate);
                sqlCmd.Parameters.Add(pUpdateDate);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                Id = Convert.ToInt32(pIdProveedor.Value);

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();

                return Id;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        // Modificar Proveedor
        public void ModificarProveedor(ProveedorBE objBE)
        {
            SqlConnection sqlConn;
            string strConn;
            SqlCommand sqlCmd;
            string strSP;

            SqlParameter pIdProveedor;
            SqlParameter pCardCode;
            SqlParameter pCardName;
            SqlParameter pTipoDocumento;
            SqlParameter pDocumento;
            SqlParameter pProceso;
            SqlParameter pIdProceso;
            SqlParameter pEstado;
            SqlParameter pUserCreate;
            SqlParameter pCreateDate;
            SqlParameter pUserUpdate;
            SqlParameter pUpdateDate;

            try
            {
                strConn = ConfigurationManager.ConnectionStrings["SICER"].ConnectionString;
                sqlConn = new SqlConnection(strConn);

                strSP = "MSS_WEB_ProveedorModificar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pIdProveedor = new SqlParameter();
                pIdProveedor.ParameterName = "@IdProveedor";
                pIdProveedor.SqlDbType = SqlDbType.Int;
                pIdProveedor.Value = objBE.IdProveedor;

                pCardCode = new SqlParameter();
                pCardCode.ParameterName = "@CardCode";
                pCardCode.SqlDbType = SqlDbType.VarChar;
                pCardCode.Size = 20;
                pCardCode.Value = objBE.CardCode;

                pCardName = new SqlParameter();
                pCardName.ParameterName = "@CardName";
                pCardName.SqlDbType = SqlDbType.VarChar;
                pCardName.Size = 100;
                pCardName.Value = objBE.CardName;

                pTipoDocumento = new SqlParameter();
                pTipoDocumento.ParameterName = "@TipoDocumento";
                pTipoDocumento.SqlDbType = SqlDbType.VarChar;
                pTipoDocumento.Size = 3;
                pTipoDocumento.Value = objBE.TipoDocumento;

                pDocumento = new SqlParameter();
                pDocumento.ParameterName = "@Documento";
                pDocumento.SqlDbType = SqlDbType.VarChar;
                pDocumento.Size = 11;
                pDocumento.Value = objBE.Documento;

                pProceso = new SqlParameter();
                pProceso.ParameterName = "@Proceso";
                pProceso.SqlDbType = SqlDbType.Int;
                pProceso.Value = objBE.Proceso;

                pIdProceso = new SqlParameter();
                pIdProceso.ParameterName = "@IdProceso";
                pIdProceso.SqlDbType = SqlDbType.Int;
                pIdProceso.Value = objBE.IdProceso;

                pEstado = new SqlParameter();
                pEstado.ParameterName = "@Estado";
                pEstado.SqlDbType = SqlDbType.Int;
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

                sqlCmd.Parameters.Add(pIdProveedor);
                sqlCmd.Parameters.Add(pCardCode);
                sqlCmd.Parameters.Add(pCardName);
                sqlCmd.Parameters.Add(pTipoDocumento);
                sqlCmd.Parameters.Add(pDocumento);
                sqlCmd.Parameters.Add(pProceso);
                sqlCmd.Parameters.Add(pIdProceso);
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
                throw;
            }
        }
    }
}