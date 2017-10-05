using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class UsuarioAreaNivelBC
    {
        public List<UsuarioAreaNivelBE> ListarUsuarioAreaNivel(int IdUsuario, int Tipo, int Tipo2)
        {
            try
            {
                UsuarioAreaNivelDA objDA = new UsuarioAreaNivelDA();
                return objDA.ListarUsuarioAreaNivel(IdUsuario, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UsuarioAreaNivelBE ObtenerUsuarioAreaNivel(int IdUsuario, int Tipo, int Tipo2)
        {
            try
            {
                UsuarioAreaNivelDA objDA = new UsuarioAreaNivelDA();
                return objDA.ObtenerUsuarioAreaNivel(IdUsuario, Tipo, Tipo2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarUsuarioAreaNivel(UsuarioAreaNivelBE objBE)
        {
            try
            {
                UsuarioAreaNivelDA objDA = new UsuarioAreaNivelDA();
                return objDA.InsertarUsuarioAreaNivel(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarUsuarioAreaNivel(UsuarioAreaNivelBE objBE)
        {
            try
            {
                UsuarioAreaNivelDA objDA = new UsuarioAreaNivelDA();
                objDA.ModificarUsuarioAreaNivel(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarUsuarioAreaNivel(int IdUsuario)
        {
            try
            {
                UsuarioAreaNivelDA objDA = new UsuarioAreaNivelDA();
                objDA.EliminarUsuarioAreaNivel(IdUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
