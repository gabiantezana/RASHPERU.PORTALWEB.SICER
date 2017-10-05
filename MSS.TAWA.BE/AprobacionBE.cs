using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class AprobacionBE
    {
        int _IdAprobacion;
        int _IdUsuarioAprobador;
        int _IdDocumento;
        String _Tipo;
        DateTime _FechaSolicitud;
        DateTime _FechaAprobacion;
        String _Estado;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdAprobacion
        {
            get { return _IdAprobacion; }
            set { _IdAprobacion = value; }
        }
        public int IdUsuarioAprobador
        {
            get { return _IdUsuarioAprobador; }
            set { _IdUsuarioAprobador = value; }
        }
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }
        public String Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public DateTime FechaSolicitud
        {
            get { return _FechaSolicitud; }
            set { _FechaSolicitud = value; }
        }
        public DateTime FechaAprobacion
        {
            get { return _FechaAprobacion; }
            set { _FechaAprobacion = value; }
        }
        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
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
