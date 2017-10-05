using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class EntregaRendirBC
    {
        public List<EntregaRendirBE> ListarEntregaRendir(int IdUsuario, int Tipo, int Tipo2, String CodigoDocumento, String Dni, String NombreSolicitante, String EsFacturable, String Estado)
        {
            try
            {
                EntregaRendirDA objDA = new EntregaRendirDA();
                return objDA.ListarEntregaRendir(IdUsuario, Tipo, Tipo2, CodigoDocumento, Dni, NombreSolicitante, EsFacturable, Estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EntregaRendirBE ObtenerEntregaRendir(int IdEntregaRendir, int Tipo)
        {
            try
            {
                EntregaRendirDA objDA = new EntregaRendirDA();
                return objDA.ObtenerEntregaRendir(IdEntregaRendir, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarEntregaRendir(EntregaRendirBE objBE)
        {
            try
            {
                EntregaRendirDA objDA = new EntregaRendirDA();
                return objDA.InsertarEntregaRendir(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarEntregaRendir(EntregaRendirBE objBE)
        {
            try
            {
                EntregaRendirDA objDA = new EntregaRendirDA();
                objDA.ModificarEntregaRendir(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
