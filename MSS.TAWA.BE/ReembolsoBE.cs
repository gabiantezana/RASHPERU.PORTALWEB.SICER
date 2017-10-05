using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class ReembolsoBE
    {
        int _IdReembolso;
        String _CodigoReembolso;
        int _IdEmpresa;
        int _IdArea;
        int _IdUsuarioCreador;
        int _IdUsuarioSolicitante;
        int _IdCentroCostos1;
        int _IdCentroCostos2;
        int _IdCentroCostos3;
        int _IdCentroCostos4;
        int _IdCentroCostos5;
        int _IdMotivo;
        int _IdMetodoPago;
        String _MontoInicial;
        String _MontoGastado;
        String _MontoReembolsado;
        String _MontoActual;
        String _Moneda;
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

        public int IdReembolso
        {
            get { return _IdReembolso; }
            set { _IdReembolso = value; }
        }
        public String CodigoReembolso
        {
            get { return _CodigoReembolso; }
            set { _CodigoReembolso = value; }
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
        public int IdCentroCostos1
        {
            get { return _IdCentroCostos1; }
            set { _IdCentroCostos1 = value; }
        }
        public int IdCentroCostos2
        {
            get { return _IdCentroCostos2; }
            set { _IdCentroCostos2 = value; }
        }
        public int IdCentroCostos3
        {
            get { return _IdCentroCostos3; }
            set { _IdCentroCostos3 = value; }
        }
        public int IdCentroCostos4
        {
            get { return _IdCentroCostos4; }
            set { _IdCentroCostos4 = value; }
        }
        public int IdCentroCostos5
        {
            get { return _IdCentroCostos5; }
            set { _IdCentroCostos5 = value; }
        }
        public int IdMotivo
        {
            get { return _IdMotivo; }
            set { _IdMotivo = value; }
        }
        public int IdMetodoPago
        {
            get { return _IdMetodoPago; }
            set { _IdMetodoPago = value; }
        }
        public String MontoInicial
        {
            get { return _MontoInicial; }
            set { _MontoInicial = value; }
        }
        public String MontoGastado
        {
            get { return _MontoGastado; }
            set { _MontoGastado = value; }
        }
        public String MontoReembolsado
        {
            get { return _MontoReembolsado; }
            set { _MontoReembolsado = value; }
        }
        public String MontoActual
        {
            get { return _MontoActual; }
            set { _MontoActual = value; }
        }
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }
        public String EsFacturable
        {
            get { return _EsFacturable; }
            set { _EsFacturable = value; }
        }
        public String MomentoFacturable
        {
            get { return _MomentoFacturable; }
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
