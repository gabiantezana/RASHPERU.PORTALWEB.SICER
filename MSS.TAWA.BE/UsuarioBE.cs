using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class UsuarioBE
    {
        int _IdUsuario;
        String _CardCode;
        String _Pass;
        String _CardName;
        String _Tipo;
        String _Phone;
        String _Mail;
        String _CantMaxCC;
        String _CantMaxER;
        String _CantMaxRE;
        int _IdPerfilUsuario;        
	    int _IdArea1;
	    int _IdArea2;
	    int _IdArea3;
	    int _IdArea4;
	    int _IdArea5;
	    int _IdCentroCostos1;
        int _IdCentroCostos2;
        int _IdCentroCostos3;
        int _IdCentroCostos4;
        int _IdCentroCostos5;
        int _IdCentroCostos6;
        int _IdCentroCostos7;
        int _IdCentroCostos8;
        int _IdCentroCostos9;
        int _IdCentroCostos10;
        int _IdCentroCostos11;
        int _IdCentroCostos12;
        int _IdCentroCostos13;
        int _IdCentroCostos14;
        int _IdCentroCostos15;
        int _IdUsuarioCC1;
        int _IdUsuarioCC2;
        int _IdUsuarioCC3;
        int _IdUsuarioER1;
        int _IdUsuarioER2;
        int _IdUsuarioER3;
        int _IdUsuarioRE1;
        int _IdUsuarioRE2;
        int _IdUsuarioRE3;
        String _Comentario;
        String _Estado;
        String _IntentoLogin;
        DateTime _HoraMinutoLogin;
        String _UserCreate;
        DateTime _CreateDate;
        String _UserUpdate;
        DateTime _UpdateDate;
        
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public String CardCode
        {
            get { return _CardCode; }
            set { _CardCode = value; }
        }
        public String Pass
        {
            get { return _Pass; }
            set { _Pass = value; }
        }
        public String CardName
        {
            get { return _CardName; }
            set { _CardName = value; }
        }
        public String Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public String Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public String Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }
        public String CantMaxCC
        {
            get { return _CantMaxCC; }
            set { _CantMaxCC = value; }
        }
        public String CantMaxER
        {
            get { return _CantMaxER; }
            set { _CantMaxER = value; }
        }
        public String CantMaxRE
        {
            get { return _CantMaxRE; }
            set { _CantMaxRE = value; }
        }
        public int IdPerfilUsuario
        {
            get { return _IdPerfilUsuario; }
            set { _IdPerfilUsuario = value; }
        }
        public int IdArea1
        {
            get { return _IdArea1; }
            set { _IdArea1 = value; }
        }
        public int IdArea2
        {
            get { return _IdArea2; }
            set { _IdArea2 = value; }
        }
        public int IdArea3
        {
            get { return _IdArea3; }
            set { _IdArea3 = value; }
        }
        public int IdArea4
        {
            get { return _IdArea4; }
            set { _IdArea4 = value; }
        }
        public int IdArea5
        {
            get { return _IdArea5; }
            set { _IdArea5 = value; }
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
        public int IdCentroCostos6
        {
            get { return _IdCentroCostos6; }
            set { _IdCentroCostos6 = value; }
        }
        public int IdCentroCostos7
        {
            get { return _IdCentroCostos7; }
            set { _IdCentroCostos7 = value; }
        }
        public int IdCentroCostos8
        {
            get { return _IdCentroCostos8; }
            set { _IdCentroCostos8 = value; }
        }
        public int IdCentroCostos9
        {
            get { return _IdCentroCostos9; }
            set { _IdCentroCostos9 = value; }
        }
        public int IdCentroCostos10
        {
            get { return _IdCentroCostos10; }
            set { _IdCentroCostos10 = value; }
        }
        public int IdCentroCostos11
        {
            get { return _IdCentroCostos11; }
            set { _IdCentroCostos11 = value; }
        }
        public int IdCentroCostos12
        {
            get { return _IdCentroCostos12; }
            set { _IdCentroCostos12 = value; }
        }
        public int IdCentroCostos13
        {
            get { return _IdCentroCostos13; }
            set { _IdCentroCostos13 = value; }
        }
        public int IdCentroCostos14
        {
            get { return _IdCentroCostos14; }
            set { _IdCentroCostos14 = value; }
        }
        public int IdCentroCostos15
        {
            get { return _IdCentroCostos15; }
            set { _IdCentroCostos15 = value; }
        }
        public int IdUsuarioCC1
        {
            get { return _IdUsuarioCC1; }
            set { _IdUsuarioCC1 = value; }
        }
        public int IdUsuarioCC2
        {
            get { return _IdUsuarioCC2; }
            set { _IdUsuarioCC2 = value; }
        }
        public int IdUsuarioCC3
        {
            get { return _IdUsuarioCC3; }
            set { _IdUsuarioCC3 = value; }
        }
        public int IdUsuarioER1
        {
            get { return _IdUsuarioER1; }
            set { _IdUsuarioER1 = value; }
        }
        public int IdUsuarioER2
        {
            get { return _IdUsuarioER2; }
            set { _IdUsuarioER2 = value; }
        }
        public int IdUsuarioER3
        {
            get { return _IdUsuarioER3; }
            set { _IdUsuarioER3 = value; }
        }
        public int IdUsuarioRE1
        {
            get { return _IdUsuarioRE1; }
            set { _IdUsuarioRE1 = value; }
        }
        public int IdUsuarioRE2
        {
            get { return _IdUsuarioRE2; }
            set { _IdUsuarioRE2 = value; }
        }
        public int IdUsuarioRE3
        {
            get { return _IdUsuarioRE3; }
            set { _IdUsuarioRE3 = value; }
        }
        public String Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }
        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public String IntentoLogin
        {
            get { return _IntentoLogin; }
            set { _IntentoLogin = value; }
        }
        public DateTime HoraMinutoLogin
        {
            get { return _HoraMinutoLogin; }
            set { _HoraMinutoLogin = value; }
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
