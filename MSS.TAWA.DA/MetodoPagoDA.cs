using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MSS.TAWA.BE;

namespace MSS.TAWA.DA
{
    public class MetodoPagoDA
    {
        // Listar MetodoPago
        public List<MetodoPagoBE> ListarMetodoPago(int Id, int Tipo, int Tipo2)
        {
            return new List<MetodoPagoBE>();
        }

        // Obtener MetodoPago
        public MetodoPagoBE ObtenerMetodoPago(int Id)
        {
            return null;
        }
    }
}