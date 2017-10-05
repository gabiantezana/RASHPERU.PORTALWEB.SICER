using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class CajaChicaDocumentoBE
    {
        int _IdCajaChicaDocumento;
        int _IdCajaChica;
        int _IdProveedor;
        int _IdConcepto;
        int _IdCentroCostos3;
        int _IdCentroCostos4;
        int _IdCentroCostos5;
        int _Rendicion;
        String _TipoDoc;
        String _SerieDoc;
        String _CorrelativoDoc;
        DateTime _FechaDoc;
        int _IdMonedaDoc;
        String _MontoDoc;
        String _TasaCambio;
        int _IdMonedaOriginal;
        String _MontoNoAfecto;
        String _MontoAfecto;
        String _MontoIGV;
        String _MontoTotal;
        String _Estado;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdCajaChicaDocumento
        {
            get { return _IdCajaChicaDocumento; }
            set { _IdCajaChicaDocumento = value; }
        }
        public int IdCajaChica
        {
            get { return _IdCajaChica; }
            set { _IdCajaChica = value; }
        }
        public int IdProveedor
        {
            get { return _IdProveedor; }
            set { _IdProveedor = value; }
        }
        public int IdConcepto
        {
            get { return _IdConcepto; }
            set { _IdConcepto = value; }
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
        public String CorrelativoDoc
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
        public String MontoDoc
        {
            get { return _MontoDoc; }
            set { _MontoDoc = value; }
        }
        public String TasaCambio
        {
            get { return _TasaCambio; }
            set { _TasaCambio = value; }
        }
        public int IdMonedaOriginal
        {
            get { return _IdMonedaOriginal; }
            set { _IdMonedaOriginal = value; }
        }
        public String MontoNoAfecto
        {
            get { return _MontoNoAfecto; }
            set { _MontoNoAfecto = value; }
        }
        public String MontoAfecto
        {
            get { return _MontoAfecto; }
            set { _MontoAfecto = value; }
        }
        public String MontoIGV
        {
            get { return _MontoIGV; }
            set { _MontoIGV = value; }
        }
        public String MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
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
