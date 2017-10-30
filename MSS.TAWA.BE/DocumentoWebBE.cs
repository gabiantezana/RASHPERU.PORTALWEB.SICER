using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class DocumentoWebBE
    {
        int _IdDocumentoWeb;
        String _CodigoDocumento;
        int _IdEmpresa;
        int _IdArea;
        int _IdUsuarioCreador;
        int _IdUsuarioSolicitante;
        String _IdCentroCostos1;
        String _IdCentroCostos2;
        String _IdCentroCostos3;
        String _IdCentroCostos4;
        String _IdCentroCostos5;
        int? _IdMetodoPago;
        Decimal _MontoInicial;
        Decimal _MontoGastado;
        Decimal _MontoActual;
        Int32 _Moneda;
        String _EsFacturable;
        String _MomentoFacturable;
        String _Asunto;
        String _Comentario;
        String _MotivoDetalle;
        DateTime _FechaSolicitud;
        DateTime _FechaContabilizacion;
        String _Estado;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;


        public Int32? IdDocumentoWebRendicionReferencia { get; set; }
        public DocumentoWebBE(TipoDocumentoWeb tipoDocumentoWeb)
        {
            TipoDocumentoWeb = tipoDocumentoWeb;
        }

        public int IdDocumentoWeb
        {
            get { return _IdDocumentoWeb; }
            set { _IdDocumentoWeb = value; }
        }

        public TipoDocumentoWeb TipoDocumentoWeb { get; set; }

        public String CodigoDocumento
        {
            get { return _CodigoDocumento; }
            set { _CodigoDocumento = value; }
        }
        public int IdEmpresa
        {
            get { return _IdEmpresa; }
            set { _IdEmpresa = value; }
        }
        public int IdArea
        {
            get { return _IdArea; }
            set { _IdArea = value; }
        }
        public int IdUsuarioCreador
        {
            get { return _IdUsuarioCreador; }
            set { _IdUsuarioCreador = value; }
        }
        public int IdUsuarioSolicitante
        {
            get { return _IdUsuarioSolicitante; }
            set { _IdUsuarioSolicitante = value; }
        }
        public String IdCentroCostos1
        {
            get { return _IdCentroCostos1 == "0" ? null : _IdCentroCostos1; }
            set { _IdCentroCostos1 = value; }
        }
        public String IdCentroCostos2
        {
            get { return _IdCentroCostos2 == "0" ? null : _IdCentroCostos2; }
            set { _IdCentroCostos2 = value; }
        }
        public String IdCentroCostos3
        {
            get { return _IdCentroCostos3 == "0" ? null : _IdCentroCostos3; }
            set { _IdCentroCostos3 = value; }
        }
        public String IdCentroCostos4
        {
            get { return _IdCentroCostos4 == "0" ? null : _IdCentroCostos4; }
            set { _IdCentroCostos4 = value; }
        }
        public String IdCentroCostos5
        {
            get { return _IdCentroCostos5 == "0" ? null : _IdCentroCostos5; }
            set { _IdCentroCostos5 = value; }
        }
        public int? IdMetodoPago
        {
            get { return _IdMetodoPago == 0 ? null : _IdMetodoPago; }
            set { _IdMetodoPago = value; }
        }
        public Decimal MontoInicial
        {
            get { return _MontoInicial; }
            set { _MontoInicial = value; }
        }
        public Decimal MontoGastado
        {
            get { return _MontoGastado; }
            set { _MontoGastado = value; }
        }
        public Decimal MontoActual
        {
            get { return _MontoActual; }
            set { _MontoActual = value; }
        }
        public Int32 Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }
        public String EsFacturable
        {
            get { return _EsFacturable == null ? String.Empty : _EsFacturable; }
            set { _EsFacturable = value; }
        }
        public String MomentoFacturable
        {
            get { return _MomentoFacturable == null ? String.Empty : _MomentoFacturable; }
            set { _MomentoFacturable = value; }
        }
        public String Asunto
        {
            get { return _Asunto; }
            set { _Asunto = value; }
        }
        public String Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }
        public String MotivoDetalle
        {
            get { return _MotivoDetalle; }
            set { _MotivoDetalle = value; }
        }
        public DateTime FechaSolicitud
        {
            get { return _FechaSolicitud; }
            set { _FechaSolicitud = value; }
        }
        public DateTime FechaContabilizacion
        {
            get { return _FechaContabilizacion; }
            set { _FechaContabilizacion = value; }
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
