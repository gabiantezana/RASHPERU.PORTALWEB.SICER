using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class PerfilUsuarioBE
    {
        int _IdPerfilUsuario;
        String _Descripcion;
        String _TipoAprobador;
        String _ModAdministrador;
        String _ModCajaChica;
        String _ModEntregaRendir;
        String _ModReembolso;
        String _CreaCajaChica;
        String _CreaEntregaRendir;
        String _CreaReembolso;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdPerfilUsuario
        {
            get { return _IdPerfilUsuario; }
            set { _IdPerfilUsuario = value; }
        }
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public String TipoAprobador
        {
            get { return _TipoAprobador; }
            set { _TipoAprobador = value; }
        }
        public String ModAdministrador
        {
            get { return _ModAdministrador; }
            set { _ModAdministrador = value; }
        }
        public String ModCajaChica
        {
            get { return _ModCajaChica; }
            set { _ModCajaChica = value; }
        }
        public String ModEntregaRendir
        {
            get { return _ModEntregaRendir; }
            set { _ModEntregaRendir = value; }
        }
        public String ModReembolso
        {
            get { return _ModReembolso; }
            set { _ModReembolso = value; }
        }
        public String CreaCajaChica
        {
            get { return _CreaCajaChica; }
            set { _CreaCajaChica = value; }
        }
        public String CreaEntregaRendir
        {
            get { return _CreaEntregaRendir; }
            set { _CreaEntregaRendir = value; }
        }
        public String CreaReembolso
        {
            get { return _CreaReembolso; }
            set { _CreaReembolso = value; }
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
