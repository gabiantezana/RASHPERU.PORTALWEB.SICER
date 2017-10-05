using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class NivelAprobacionBE
    {
        int _IdNivel;
        String _Descripcion;
        String _Nivel;
        String _Documento;
        String _EsDeMonto;
        String _Monto;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdNivel
        {
            get { return _IdNivel; }
            set { _IdNivel = value; }
        }
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public String Nivel
        {
            get { return _Nivel; }
            set { _Nivel = value; }
        }
        public String Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        public String EsDeMonto
        {
            get { return _EsDeMonto; }
            set { _EsDeMonto = value; }
        }
        public String Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
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
