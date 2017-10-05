using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class EstadoBE
    {
        int  _EstadoCode;
        String _EstadoNombre;
        String _EstadoDescripcion;
      
        
        public int EstadoCode
        {
            get { return _EstadoCode; }
            set { _EstadoCode = value; }
        }
        public String EstadoNombre
        {
            get { return _EstadoNombre; }
            set { _EstadoNombre = value; }
        }
        public String EstadoDescripcion
        {
            get { return _EstadoDescripcion; }
            set { _EstadoDescripcion = value; }
        }
       
    }
}
