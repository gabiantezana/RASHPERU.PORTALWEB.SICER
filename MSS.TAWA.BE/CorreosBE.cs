using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSS.TAWA.BE
{
    public class CorreosBE
    {
        int _Id;
        String _correo;
        String _TextoCorreo;


        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public String Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        public String TextoCorreo
        {
            get { return _TextoCorreo; }
            set { _TextoCorreo = value; }
        }

    }
}
