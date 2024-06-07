using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Presentation.Cotizaciones.Models
{
    public class Descuento
    {
        public int idproducto { get; set; }
        public int iddetalle { get; set; }
        public double PrecioNormal { get; set; }
        public double Cantidad { get; set; }
    }
}
