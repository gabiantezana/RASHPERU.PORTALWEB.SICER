using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class CajaChicaBC
    {
        public List<CajaChicaBE> ListarCajaChica(int IdUsuario, int Tipo, int Tipo2, String CodigoDocumento, String Dni, String NombreSolicitante, String EsFacturable, String Estado)
        {
            try
            {
                CajaChicaDA objDA = new CajaChicaDA();
                return objDA.ListarCajaChica(IdUsuario, Tipo, Tipo2, CodigoDocumento , Dni , NombreSolicitante , EsFacturable, Estado );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CajaChicaBE ObtenerCajaChica(int IdCajaChica, int Tipo)
        {
            try
            {
                CajaChicaDA objDA = new CajaChicaDA();
                return objDA.ObtenerCajaChica(IdCajaChica, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarCajaChica(CajaChicaBE objBE)
        {
            try
            {
                CajaChicaDA objDA = new CajaChicaDA();
                return objDA.InsertarCajaChica(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarCajaChica(CajaChicaBE objBE)
        {
            try
            {
                CajaChicaDA objDA = new CajaChicaDA();
                objDA.ModificarCajaChica(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
