using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class Lgastos
    {
        public int Idgasto { get; set; }
        public DateTime Fecha { get; set; }
        public string NroDocumento { get; set; }
        public string TipoComprobante { get; set; }
        public double Importe { get; set; }
        public string Descripcion { get; set; }
        public int idcaja { get; set; }
        public int Idconceptos { get; set; }
        public string Rnc { get; set; }
        public string Cedula { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroComprobante { get; set; }
        public string NroComprobanteModi { get; set; }
        public string ModoPago { get; set; }
        public string TipoGasto { get; set; }
    }
}
