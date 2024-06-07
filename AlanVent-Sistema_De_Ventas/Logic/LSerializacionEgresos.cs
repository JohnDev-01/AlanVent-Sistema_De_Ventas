using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class LSerializacionEgresos
    {
        public string Serie { get; set; }
        public string TipoComprobante { get; set; }
        public int CantidadCeros { get; set; }
        public long CantidadSinCeros { get; set; }
    }
}
