using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class ProveedorBE
    {
        int _IdProveedor;
        String _CardCode;
        String _CardName;
        String _TipoDocumento;
        String _Documento;
        int _Proceso;
        int _IdProceso;
        int _Estado;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdProveedor
        {
            get { return _IdProveedor; }
            set { _IdProveedor = value; }
        }
        public String CardCode
        {
            get { return _CardCode; }
            set { _CardCode = value; }
        }
        public String CardName
        {
            get { return _CardName; }
            set { _CardName = value; }
        }
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }
        public String Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        public int Proceso
        {
            get { return _Proceso; }
            set { _Proceso = value; }
        }
        public int IdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public int Estado
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
