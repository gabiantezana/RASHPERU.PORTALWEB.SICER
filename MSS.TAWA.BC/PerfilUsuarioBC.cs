using System;
using System.Collections.Generic;
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
                var objDA = new PerfilUsuarioDA();
                return objDA.ListarPerfilUsuario();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PerfilUsuarioBE ObtenerPerfilUsuario(int Id)
        {
            try
            {
                var objDA = new PerfilUsuarioDA();
                return objDA.ObtenerPerfilUsuario(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarPerfilUsuario(PerfilUsuarioBE objBE)
        {
            try
            {
                var objDA = new PerfilUsuarioDA();
                return objDA.InsertarPerfilUsuario(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarPerfilUsuario(PerfilUsuarioBE objBE)
        {
            try
            {
                var objDA = new PerfilUsuarioDA();
                objDA.ModificarPerfilUsuario(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}