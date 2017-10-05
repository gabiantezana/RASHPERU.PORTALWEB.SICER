using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class CentroCostosBE
    {
        int _IdCentroCostos;
        int _Nivel;
        String _CodigoSAP;
        String _Descripcion;
        int _IdEmpresa;
        String _Concepto;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;

        public int IdCentroCostos
        {
            get { return _IdCentroCostos; }
            set { _IdCentroCostos = value; }
        }
        public int Nivel
        {
            get { return _Nivel; }
            set { _Nivel = value; }
        }
        public String CodigoSAP
        {
            get { return _CodigoSAP; }
            set { _CodigoSAP = value; }
        }
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public int IdEmpresa
        {
            get { return _IdEmpresa; }
            set { _IdEmpresa = value; }
        }
        public String Concepto
        {
            get { return _Concepto; }
            set { _Concepto = value; }
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
