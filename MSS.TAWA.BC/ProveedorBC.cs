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

        public string GetCardNameProveedorSAP(string Nombre)
        {
            var objDA = new ProveedorDA();
            return objDA.ObtenerProveedorDeSAP(Nombre);
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