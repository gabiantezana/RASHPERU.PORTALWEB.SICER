using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class ControlContabilidadBE
    {

        int _IdDocumento;
        String _CodigoDocumento;
        int _UserUpdate;
        DateTime _CreateDate;
        DateTime _FechaContabilizacion;
        DateTime _Getdate;
        String _Documento;
        String _Hostname;
        String _IP;


        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        public String CodigoDocumento
        {
            get { return _CodigoDocumento; }
            set { _CodigoDocumento = value; }
        }

        public int UserUpdate
        {
            get { return _UserUpdate; }
            set { _UserUpdate = value; }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public DateTime FechaContabilizacion
        {
            get { return _FechaContabilizacion; }
            set { _FechaContabilizacion = value; }
        }

        public DateTime Getdate
        {
            get { return _Getdate; }
            set { _Getdate = value; }
        }
        public String Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        public String Hostname
        {
            get { return _Hostname; }
            set { _Hostname = value; }
        }
        public String IP
        {
            get { return _IP; }
            set { _IP = value; }
        }
    }
}
