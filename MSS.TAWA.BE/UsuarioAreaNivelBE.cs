using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class UsuarioAreaNivelBE
    {
        int _Id;
        int _IdUsuario;
        int _IdArea;
        int _IdNivelAprobacion;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public int IdArea
        {
            get { return _IdArea; }
            set { _IdArea = value; }
        }
        public int IdNivelAprobacion
        {
            get { return _IdNivelAprobacion; }
            set { _IdNivelAprobacion = value; }
        }
        public String UserCreate
        {
            get { return _UserCreate; }
            set { _UserCreate = value; }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        public String UserUpdate
        {
            get { return _UserUpdate; }
            set { _UserUpdate = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
    }
}
