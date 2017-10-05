using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class GrupoTrabajoBE
    {
        int _Id;
        int _IdUsuarioNivel;
        int _IdUsuarioSubNivel;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int IdUsuarioNivel
        {
            get { return _IdUsuarioNivel; }
            set { _IdUsuarioNivel = value; }
        }
        public int IdUsuarioSubNivel
        {
            get { return _IdUsuarioSubNivel; }
            set { _IdUsuarioSubNivel = value; }
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
