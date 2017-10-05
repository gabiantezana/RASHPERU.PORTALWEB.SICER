using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MSS.TAWA.BE;
using MSS.TAWA.DA;

namespace MSS.TAWA.BC
{
    public class GrupoTrabajoBC
    {
        public List<GrupoTrabajoBE> ListarGrupoTrabajo(int Id, int Tipo)
        {
            try
            {
                GrupoTrabajoDA objDA = new GrupoTrabajoDA();
                return objDA.ListarGrupoTrabajo(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GrupoTrabajoBE ObtenerGrupoTrabajo(int Id)
        {
            try
            {
                GrupoTrabajoDA objDA = new GrupoTrabajoDA();
                return objDA.ObtenerGrupoTrabajo(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarGrupoTrabajo(GrupoTrabajoBE objBE)
        {
            try
            {
                GrupoTrabajoDA objDA = new GrupoTrabajoDA();
                return objDA.InsertarGrupoTrabajo(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarGrupoTrabajo(GrupoTrabajoBE objBE)
        {
            try
            {
                GrupoTrabajoDA objDA = new GrupoTrabajoDA();
                objDA.ModificarGrupoTrabajo(objBE);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
