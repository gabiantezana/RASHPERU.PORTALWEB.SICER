using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class PerfilUsuarioBC
    {
        public List<PerfilUsuarioBE> ListarPerfilUsuario()
        {
            try
            {
                PerfilUsuarioDA objDA = new PerfilUsuarioDA();
                return objDA.ListarPerfilUsuario();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PerfilUsuarioBE ObtenerPerfilUsuario(int Id)
        {
            try
            {
                PerfilUsuarioDA objDA = new PerfilUsuarioDA();
                return objDA.ObtenerPerfilUsuario(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarPerfilUsuario(PerfilUsuarioBE objBE)
        {
            try
            {
                PerfilUsuarioDA objDA = new PerfilUsuarioDA();
                return objDA.InsertarPerfilUsuario(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarPerfilUsuario(PerfilUsuarioBE objBE)
        {
            try
            {
                PerfilUsuarioDA objDA = new PerfilUsuarioDA();
                objDA.ModificarPerfilUsuario(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
