using MSS.TAWA.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MSS.TAWA.DA
{
   public class CuentasContablesDevolucionDA
    {
        // Listar Concepto
        public List<CuentaContableDevolucionBE> ListarCuentas()
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
                strSP = "MSS_WEB_CuentasContablesDevolucionListar";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                List<CuentaContableDevolucionBE> lstCuentasBE;
                CuentaContableDevolucionBE objCuentasBE;
                lstCuentasBE = new List<CuentaContableDevolucionBE>();

                while (sqlDR.Read())
                {
                    objCuentasBE = new CuentaContableDevolucionBE();
                    objCuentasBE.U_Codigo = sqlDR.GetString(sqlDR.GetOrdinal("U_Codigo"));
                    objCuentasBE.U_Descripcion = sqlDR.GetString(sqlDR.GetOrdinal("U_Descripcion"));
                    objCuentasBE.U_CuentaContable= sqlDR.GetString(sqlDR.GetOrdinal("U_CuentaContable"));
                    lstCuentasBE.Add(objCuentasBE);
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return lstCuentasBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Obtener Concepto
        public CuentaContableDevolucionBE ObtenerCuentaContable(String codigo)
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
                strSP = "MSS_WEB_CuentaContableDevolucionObtener";
                sqlCmd = new SqlCommand(strSP, sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                pId = new SqlParameter();
                pId.ParameterName = "@U_Codigo";
                pId.SqlDbType = SqlDbType.NVarChar;
                pId.Value = codigo;
                sqlCmd.Parameters.Add(pId);

                sqlCmd.Connection.Open();
                sqlDR = sqlCmd.ExecuteReader();

                CuentaContableDevolucionBE cuentaContableBE;
                cuentaContableBE = null;

                while (sqlDR.Read())
                {
                    cuentaContableBE = new CuentaContableDevolucionBE();
                    cuentaContableBE.U_Codigo = sqlDR.GetString(sqlDR.GetOrdinal("U_Codigo"));
                    cuentaContableBE.U_Descripcion= sqlDR.GetString(sqlDR.GetOrdinal("U_Descripcion"));
                    cuentaContableBE.U_CuentaContable = sqlDR.GetString(sqlDR.GetOrdinal("U_CuentaContable"));
                }

                sqlCmd.Connection.Close();
                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return cuentaContableBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
