using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class ConceptoBE
    {
        int _IdConcepto;
        String _Descripcion;
        String _CuentaContable;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdConcepto
        {
            get { return _IdConcepto; }
            set { _IdConcepto = value; }
        }
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public String CuentaContable
        {
            get { return _CuentaContable; }
            set { _CuentaContable = value; }
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
