using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class DocumentoWebRendicionBE
    {
        int _IdDocumentoWebRendicion;
        int _IdDocumentoWeb;
        int _IdProveedor;
        string _SAPProveedor;
        String _IdConcepto;
        String _IdCentroCostos1;
        String _IdCentroCostos2;
        String _IdCentroCostos3;
        String _IdCentroCostos4;
        String _IdCentroCostos5;
        int _Rendicion;
        /// <summary>
        /// TODO: Partida presupuestal
        /// </summary>
        String _codigoPartidaPresupuestal;

        String _TipoDoc;
        String _SerieDoc;
        Int32 _CorrelativoDoc;
        DateTime _FechaDoc;
        int _IdMonedaDoc;
        Decimal _MontoDoc;
        Decimal _TasaCambio;
        int _IdMonedaOriginal;
        Decimal _MontoNoAfecto;
        Decimal _MontoAfecto;
        Decimal _MontoIGV;
        Decimal _MontoTotal;
        String _Estado;
        Int32? _UserCreate;
        DateTime _CreateDate;
        Int32? _UserUpdate;
        DateTime _UpdateDate;
        String _CodigoCuentaContableDevolucion;

        public string SAPProveedor
        {
            get { return _SAPProveedor; }
            set { _SAPProveedor = value; }
        }
        public int IdDocumentoWebRendicion
        {
            get { return _IdDocumentoWebRendicion; }
            set { _IdDocumentoWebRendicion = value; }
        }
        public int IdDocumentoWeb
        {
            get { return _IdDocumentoWeb; }
            set { _IdDocumentoWeb = value; }
        }
        /*
        public int IdProveedor
        {
            get { return _IdProveedor; }
            set { _IdProveedor = value; }
        }*/
        public String IdConcepto
        {
            get { return _IdConcepto; }
            set { _IdConcepto = value; }
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
        public int Rendicion
        {
            get { return _Rendicion; }
            set { _Rendicion = value; }
        }
        public String TipoDoc
        {
            get { return _TipoDoc; }
            set { _TipoDoc = value; }
        }
        public String SerieDoc
        {
            get { return _SerieDoc; }
            set { _SerieDoc = value; }
        }
        public Int32 CorrelativoDoc
        {
            get { return _CorrelativoDoc; }
            set { _CorrelativoDoc = value; }
        }
        public DateTime FechaDoc
        {
            get { return _FechaDoc; }
            set { _FechaDoc = value; }
        }
        public int IdMonedaDoc
        {
            get { return _IdMonedaDoc; }
            set { _IdMonedaDoc = value; }
        }
        public Decimal MontoDoc
        {
            get { return _MontoDoc; }
            set { _MontoDoc = value; }
        }
        public Decimal TasaCambio
        {
            get { return _TasaCambio; }
            set { _TasaCambio = value; }
        }
        public int IdMonedaOriginal
        {
            get { return _IdMonedaOriginal; }
            set { _IdMonedaOriginal = value; }
        }
        public Decimal MontoNoAfecto
        {
            get { return _MontoNoAfecto; }
            set { _MontoNoAfecto = value; }
        }
        public Decimal MontoAfecto
        {
            get { return _MontoAfecto; }
            set { _MontoAfecto = value; }
        }
        public Decimal MontoIGV
        {
            get { return _MontoIGV; }
            set { _MontoIGV = value; }
        }
        public Decimal MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
        }
        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public Int32? UserCreate
        {
            get { return _UserCreate; }
            set { _UserCreate = value; }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        public Int32? UserUpdate
        {
            get { return _UserUpdate; }
            set { _UserUpdate = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public String CodigoPartidaPresupuestal
        {
            get { return _codigoPartidaPresupuestal == "0" ? null : _codigoPartidaPresupuestal; }
            set { _codigoPartidaPresupuestal = value; }
        }

        public String CodigoCuentaContableDevolucion
        {
            get { return _CodigoCuentaContableDevolucion; }
            set { _CodigoCuentaContableDevolucion = value; }
        }

    }
}
