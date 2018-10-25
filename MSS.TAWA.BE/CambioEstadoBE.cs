using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.TAWA.BE
{
    public class CambioEstadoBE
    {
        public Int32 IdUsuario { get; set; }
        public Int32 IdDocumentoWeb { get; set; }
        public String Comentario { get; set; }
        public int TipoDocumentoOrigen { get; set; }//1:APERTURA,2:RENDICIÓN
    }
}
