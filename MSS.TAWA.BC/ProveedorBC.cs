using System;
using System.Collections.Generic;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class ProveedorBC
    {
        public List<ProveedorBE> ListarProveedor(int Id, int Tipo)
        {
            try
            {
                var objDA = new ProveedorDA();
                return objDA.ListarProveedor(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProveedorBE> ListarProveedoresDeSAP()
        {
            return new ProveedorDA().ListarProveedoresDeSAP();
        }

        public string GetCardNameProveedorSAP(string Nombre)
        {
            var objDA = new ProveedorDA();
            return objDA.ObtenerProveedorDeSAP(Nombre);
        }

        public string GetCardCodeProveedorSAP(string ruc)
        {
            return new ProveedorDA().GetCardCodeProveedorSAP(ruc);
        }

        public ProveedorBE ObtenerProveedor(int Id, int Tipo, string Nombre)
        {
            try
            {
                var objDA = new ProveedorDA();
                return objDA.ObtenerProveedor(Id, Tipo, Nombre);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ProveedorBE ObtenerProveedorPorDocumento(string documento)
        {
            try
            {
                var objDA = new ProveedorDA();
                return objDA.ObtenerProveedorPorDocumento(documento);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarProveedor(ProveedorBE objBE)
        {
            try
            {
                var objDA = new ProveedorDA();
                return objDA.InsertarProveedor(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarProveedor(ProveedorBE objBE)
        {
            try
            {
                var objDA = new ProveedorDA();
                objDA.ModificarProveedor(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}