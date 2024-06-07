using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class Lcompras
    {
        public int Idcompra { get; set; }
        public DateTime fechacompra { get; set; }
        public double Total { get; set; }
        public string Comprobante { get; set; }
        public int IdProveedor { get; set; }
        public int Idcaja { get; set; }
        public double  Efectivo { get; set; }
        public double Credito { get; set; }
        public string TipoPago { get; set; }
        public double Impuestos { get; set; }
        public double Subtotal { get; set; }
        public string  Modopago { get; set; }
        public string  NumeroComprobante { get; set; }
        public double ImpuestoRetenido { get; set; }

    }
}
