using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class ReembolsoBC
    {
        public List<ReembolsoBE> ListarReembolso(int IdUsuario, int Tipo, int Tipo2, String CodigoDocumento, String Dni, String NombreSolicitante, String EsFacturable, String Estado)
        {
            try
            {
                ReembolsoDA objDA = new ReembolsoDA();
                return objDA.ListarReembolso(IdUsuario, Tipo, Tipo2, CodigoDocumento, Dni, NombreSolicitante, EsFacturable, Estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ReembolsoBE ObtenerReembolso(int IdReembolso, int Tipo)
        {
            try
            {
                ReembolsoDA objDA = new ReembolsoDA();
                return objDA.ObtenerReembolso(IdReembolso, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarReembolso(ReembolsoBE objBE)
        {
            try
            {
                ReembolsoDA objDA = new ReembolsoDA();
                return objDA.InsertarReembolso(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarReembolso(ReembolsoBE objBE)
        {
            try
            {
                ReembolsoDA objDA = new ReembolsoDA();
                objDA.ModificarReembolso(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
