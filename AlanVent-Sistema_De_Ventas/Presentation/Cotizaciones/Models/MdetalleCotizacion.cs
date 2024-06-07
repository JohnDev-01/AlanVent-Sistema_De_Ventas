using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Models
{
    public class MdetalleCotizacion
    {
        public int Iddetalle { get; set; }
        public int Idventa { get; set; }
        public int Idproducto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Importe { get; set; }
        public double Impuesto { get; set; }
        public double Descuento { get; set; }
    }
}
