using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                ProveedorDA objDA = new ProveedorDA();
                return objDA.ListarProveedor(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProveedorBE ObtenerProveedor(int Id, int Tipo, string Nombre)
        {
            try
            {
                ProveedorDA objDA = new ProveedorDA();
                return objDA.ObtenerProveedor(Id, Tipo, Nombre);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarProveedor(ProveedorBE objBE)
        {
            try
            {
                ProveedorDA objDA = new ProveedorDA();
                return objDA.InsertarProveedor(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarProveedor(ProveedorBE objBE)
        {
            try
            {
                ProveedorDA objDA = new ProveedorDA();
                objDA.ModificarProveedor(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
