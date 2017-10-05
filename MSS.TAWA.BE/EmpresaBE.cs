using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class EmpresaBE
    {
        int _IdEmpresa;
        String _Descripcion;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;
        Decimal _MontoRedondeo;
        String _CorreoTesoreria;

        public int IdEmpresa
        {
            get { return _IdEmpresa; }
            set { _IdEmpresa = value; }
        }
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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

        public Decimal MontoRedondeo
        {
            get { return _MontoRedondeo; }
            set { _MontoRedondeo = value; }
        }

        public String CorreoTesoreria
        {
            get { return _CorreoTesoreria; }
            set { _CorreoTesoreria = value; }
        }
    }
}
