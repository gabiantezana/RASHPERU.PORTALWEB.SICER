using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class NivelSeguridadBE
    {
        int _IdNivelSeguridad;
        String _NivelSeguridad;
        bool _Activo;
        bool _CaracterNumerico;
        bool _CaracterMayuscula;
        bool _CaracterEspecial;
        int _DiasVencimiento;
        int _NoRepetirContrasena;
        int _NumNumericos;
        int _NumMayusculas;
        int _NumEspeciales;
        int _NumCarContrasena;
        
        public int IdNivel
        {
            get { return _IdNivelSeguridad; }
            set { _IdNivelSeguridad = value; }
        }
        public String NivelSeguridad
        {
            get { return _NivelSeguridad; }
            set { _NivelSeguridad = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public bool CaracterNumerico
        {
            get { return _CaracterNumerico; }
            set { _CaracterNumerico = value; }
        }


        public bool CaracterMayuscula
        {
            get { return _CaracterMayuscula; }
            set { _CaracterMayuscula = value; }
        }
        public bool CaracterEspecial
        {
            get { return _CaracterEspecial; }
            set { _CaracterEspecial = value; }
        }
        public int DiasVencimiento
        {
            get { return _DiasVencimiento; }
            set { _DiasVencimiento = value; }
        }
        public int NoRepetirContrasena
        {
            get { return _NoRepetirContrasena; }
            set { _NoRepetirContrasena = value; }
        }

        public int NumNumericos
        {
            get { return _NumNumericos; }
            set { _NumNumericos = value; }
        }
        public int NumMayusculas
        {
            get { return _NumMayusculas; }
            set { _NumMayusculas = value; }
        }
        public int NumEspeciales
        {
            get { return _NumEspeciales; }
            set { _NumEspeciales = value; }
        }
        public int NumCarContrasena
        {
            get { return _NumCarContrasena; }
            set { _NumCarContrasena = value; }
        }
    }
}
