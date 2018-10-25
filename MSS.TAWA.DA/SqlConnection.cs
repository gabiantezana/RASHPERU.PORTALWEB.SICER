using MSS.TAWA.HP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.TAWA.DA
{
    public class SQLConnection
    {
        ADODB.Recordset _Recordset { get; set; }
        private static ADODB.Connection _Connection { get; set; }

        public ADODB.Recordset DoQuery(String SQLquery)
        {
            try
            {
                String _connectionStringFromEF = System.Configuration.ConfigurationManager.ConnectionStrings["SQLRECORDSETS"].ConnectionString;
                String _connectionString = "Provider = SQLOLEDB; " + _connectionStringFromEF; // Data Source = LAPTOP-GAP\\SQL2012; Initial Catalog = SICER_INT_SBO;User ID = sa; Password = root";

                _Connection = new ADODB.Connection();
                _Recordset = new ADODB.Recordset();

                _Connection.Open(_connectionString, String.Empty, String.Empty, -1);
                object obj = new object();
                _Recordset = _Connection.Execute("SET NOCOUNT ON; " + SQLquery, out obj);
                //_Connection.Close();

                return _Recordset;
            }
            catch (Exception ex)
            {
                Exception _ex = new Exception("SET NOCOUNT ON; " + SQLquery);
                ExceptionHelper.LogException(ex);
                throw;
            }
            finally
            {
                /* System.Runtime.InteropServices.Marshal.ReleaseComObject(_Connection);
                 System.Runtime.InteropServices.Marshal.ReleaseComObject(_Recordset);
                 _Connection = null;
                 _Recordset = null;

                 GC.Collect();
                 GC.WaitForPendingFinalizers();
                 */
            }
        }
    }
}
