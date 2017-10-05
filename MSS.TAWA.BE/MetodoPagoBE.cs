using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class MetodoPagoBE
    {
        int _IdMetodoPago;
        int _IdEmpresa;
        String _PayMethCod;
        String _Descripcion;
	    String _UserCreate;
	    DateTime _CreateDate;
	    String _UserUpdate;
        DateTime _UpdateDate;

        public int IdMetodoPago
        {
            get { return _IdMetodoPago; }
            set { _IdMetodoPago = value; }
        }
        public int IdEmpresa
        {
            get { return _IdEmpresa; }
            set { _IdEmpresa = value; }
        }
        public String PayMethCod
        {
            get { return _PayMethCod; }
            set { _PayMethCod = value; }
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
    }
}
